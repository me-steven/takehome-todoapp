

<script>
import { ref } from 'vue'
import axios from 'axios'
import { v4 as uuidv4 } from 'uuid';

export default {
    data() {
        return {
            searchQuery: ref(''),
            newTaskName: '',
            newTaskDescription: '',
            filter: {
                isActive: true
            },
            tasks: [
                {epochTime: 1745816206, uuid: '00000000-0000-0000-0000-000000000000',  name: "Task 1", description: "This is a task", isActive: true},
                {epochTime: 1745816206, uuid: '00000000-0000-0000-0000-000000000000',  name: "Task 2", description: "This is a task", isActive: false},
                {epochTime: 1745816206, uuid: '00000000-0000-0000-0000-000000000000',  name: "Task 3", description: "This is a task", isActive: true},
                {epochTime: 1745816206, uuid: '00000000-0000-0000-0000-000000000000',  name: "Task 4", description: "This is a task", isActive: false},
                {epochTime: 1745816206, uuid: '00000000-0000-0000-0000-000000000000',  name: "Task 5", description: "This is a task", isActive: true},
                {epochTime: 1745816206, uuid: '00000000-0000-0000-0000-000000000000',  name: "Task 6", description: "This is a task", isActive: true},
            ],
        };
    },
    computed: {
        activeTasks() {
            return this.tasks.filter(task => 
            task.isActive && task.name.toLowerCase().includes(this.searchQuery.toLowerCase())
        )
        },
        completedTasks() {
            return this.tasks.filter(task => 
            !task.isActive && task.name.toLowerCase().includes(this.searchQuery.toLowerCase())
        )
        },

    },
    methods: {
        async updateProgress(uuid, changingTask) {
            try {
                changingTask.isActive = !changingTask.isActive;
                const response = await axios.put(`/api/tasks/${uuid}`, changingTask);
                if (response.status === 200) {
                    this.tasks = this.tasks.map(task => {
                        if (task.uuid === uuid) {
                            return task;
                        }
                        return task;
                    });
                }
            } catch (error) {
                console.log(error);
            }
        },
        async deleteTask(uuid) {
            try {
                const response = await axios.delete(`/api/tasks/${uuid}`);
                if (response.status === 204) {
                    this.tasks = this.tasks.filter(task => task.uuid !== uuid);
                }

            } catch (error) {
                console.log(error);
            }
            
        },
        async addTask() {
            this.newTask = {
                epochTime: Date.now(),
                uuid: uuidv4(),
                name: this.newTaskName.trim(),
                description: this.newTaskDescription.trim(),
                isActive: true,
                
            };

            try {
            const response = await axios.post(`/api/tasks`, this.newTask);

            if (response.status === 201 && response.data) {
                const returnTask = {
                    uuid: response.data.uuid,
                    name: response.data.name,
                    description: response.data.description,
                    isActive: response.data.isActive,
                    epochTime: response.data.epochTime,
                }

            this.tasks.push(returnTask);
            this.newTaskName = this.newTaskDescription = '';
            }
        } catch (error){
            console.log(error);
        }
        },
        async fetchTasks() {
                try {
                    const response = await axios.get(`/api/tasks`);
                    this.tasks = response.data.map(apiTask => {
                        return {
                            uuid: apiTask.uuid,
                            name: apiTask.name,
                            description: apiTask.description,
                            isActive: apiTask.isActive,
                            epochTime: apiTask.epochTime,
                        }
                    })


                } catch (error) {
                    console.log(error);
                }

            },
    },
    created() {
        this.fetchTasks()
    }

}

</script>

<template>
    <div id="todo-list">
        <h1>Things To Do</h1>
        <table>
            <tbody>
                <tr id="filterBar">
                    <td><input type="text" v-model="searchQuery" placeholder="Search for tasks" /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
        <table id="taskList">
            <thead>
                <tr id="subHeader">
                    <th class="taskName">
                        Task Name
                    </th>
                    <th class="taskDescription">
                        Description
                    </th>

                    <th class="action">
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                
                <tr class="completionHeader">In Progress</tr>
                <tr v-for="task in activeTasks" :key="task.uuid" class="inProgress content">
                    <td class="taskName">{{ task.name }}</td>
                    <td class="taskDescription">{{ task.description }}</td>
                    <td class="updateProgress">
                        <button @click="updateProgress(task.uuid, task)">
                            {{ task.isActive ? "Mark Completed" : "Mark InProgress" }}
                        </button>
                    </td>
                    <td class="delete">
                        <button @click="deleteTask(task.uuid)">Delete</button>
                    </td>
                </tr>
            </tbody>
            <tbody>
                <tr class="completionHeader">Completed</tr>
                <tr v-for="task in completedTasks" :key="task.uuid" class="completed content">
                    <td class="taskName">{{ task.name }}</td>
                    <td class="taskDescription">{{ task.description }}</td>
                    <td class="updateProgress">
                        <button @click="updateProgress(task.uuid, task)">
                      {{ task.isActive ? "Mark Completed" : "Mark In Progress" }}
                        </button>
                    </td>
                    <td class="delete">
                        <button @click="deleteTask(task.uuid)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <tr id="addTask">
            <td class="taskName">
                <input type="text" v-model="newTaskName" placeholder="New Task Name" />
            </td>
            <td class="taskDescription">
                <input type="text" v-model="newTaskDescription" placeholder="New Task Description" />
            </td>
            <td class="action">
                <button  text="Add Task" @click="addTask" >Add Task</button>
            </td>
        </tr>

    </div>
</template>


<style scoped>

h1 {
    text-align: center;
}

table#taskList {
    justify-content: center;
    align-items: top;
    border: 2px dashed grey;
    background-color: rgba(200,200,200, .1);
    min-width: 100%;
    padding: 1px;
    margin: 3px;
}

thead {
    display: flex;
    background-color: rgba(230,230, 230,.2);
}

tr {
    display: grid;
    justify-content: space-between;
    width: 100%;
}

td {
    display: flex;
    justify-content: space-between;
    padding: 3px;
    margin: 3px;
}

.content {
    display: flex;
    justify-content: space-between;
}

.completionHeader {
    border-top: 2px dashed grey;
    border-bottom: 2px dashed grey;
}

.inProgress {
    background-color: rgba(230,230, 230,.2);
}

.completed {
    background-color: rgba(100,200,100,.5);
}

.taskName {
    display: flex;
    grid-area: taskName;
}

.taskDescription {
    display: flex;
    grid-area: taskDescription;
}

.delete {
    display: flex;
    grid-area: delete;
}

.updateProgress {
    display: flex;
    grid-area: update;
}

.content {
    display: grid;
    grid-template-areas: "taskName taskDescription update delete";
    grid-template-columns: 1fr 3fr 1fr 1fr;
    justify-content: space-around;
}

.action {
    display: flex;
    grid-area: action;
}

#subHeader {
    display: grid;
    grid-template-areas: "taskName taskDescription action";
    grid-template-columns: 1fr 3fr 1fr;
    justify-content: space-around;
}

#addTask {
    display: grid;
    grid-template-areas: "taskName taskDescription action";
    grid-template-columns: 1fr 3fr 1fr;
    justify-content: space-around;
}


</style>