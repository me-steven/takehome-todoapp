<script>
import { ref } from 'vue'

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
                { id: 1, name: "Task 1", description: "This is a task", isActive: true },
                { id: 2, name: "Task 2", description: "This is a task", isActive: false },
                { id: 3, name: "Task 3", description: "This is a task", isActive: true },
                { id: 4, name: "Task 4", description: "This is a task", isActive: false},
                { id: 5, name: "Task 5", description: "This is a task", isActive: true },
                { id: 6, name: "Task 6", description: "This is a task", isActive: true },
            ]
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
        deleteTask(id) {
            this.tasks = this.tasks.filter(task => task.id !== id);
        },
        addTask() {
            this.newTask = {
                id: this.tasks.length + 1,
                name: this.newTaskName.trim(),
                description: this.newTaskDescription.trim(),
                isActive: true
            };
            this.tasks.push(this.newTask);
            this.newTaskName = this.newTaskDescription = '';
        }
    }
}
</script>

<template>
    <div id="todo-list">
        <h1>Things To Do</h1>
        <tr id="filterBar">
                    <td><input type="text" v-model="searchQuery" placeholder="Search for tasks" /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
        <table>
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
                <tr v-for="task in activeTasks" :key="task.id" class="inProgress content">
                    <td class="taskName">{{ task.name }}</td>
                    <td class="taskDescription">{{ task.description }}</td>
                    <td class="updateProgress">
                        <button @click="task.isActive = !task.isActive">
                            {{ task.isActive ? "Mark Completed" : "Mark InProgress" }}
                        </button>
                    </td>
                    <td class="delete">
                        <button @click="deleteTask(task.id)">Delete</button>
                    </td>
                </tr>
            </tbody>
            <tbody>
                <tr class="completionHeader">Completed</tr>
                <tr v-for="task in completedTasks" :key="task.id" class="completed content">
                    <td class="taskName">{{ task.name }}</td>
                    <td class="taskDescription">{{ task.description }}</td>
                    <td class="updateProgress">
                        <button @click="task.isActive = !task.isActive">
                      {{ task.isActive ? "Mark Completed" : "Mark In Progress" }}
                        </button>
                    </td>
                    <td class="delete">
                        <button @click="deleteTask(task.id)">Delete</button>
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

table {
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