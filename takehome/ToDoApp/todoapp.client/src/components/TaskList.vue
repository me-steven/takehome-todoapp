<script>
import { ref } from 'vue'

export default {
    data() {
        return {
            searchQuery: ref(''),
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

    }
}
</script>

<template>
    <div id="todo-list">
        <h1>Things To Do</h1>
        <table>
            <thead>
                <tr id="subHeader">
                    <th>
                        Task Name
                    </th>
                    <th colspan="2">
                        Description
                    </th>
                    <th>
                        Action
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr id="filterBar">
                    <td><input type="text" v-model="searchQuery" placeholder="Search for tasks" /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr class="completionHeader">In Progress</tr>
                <tr v-for="task in activeTasks" :key="task.id" class="inProgress content">
                    <td class="taskName">{{ task.name }}</td>
                    <td class="taskDescription">{{ task.description }}</td>
                    <td class="action">
                        <button @click="task.isActive = !task.isActive">
                            {{ task.isActive ? "Mark Completed" : "Mark InProgress" }}
                        </button>
                    </td>
                </tr>
            </tbody>
            <tbody>
                <tr class="completionHeader">Completed</tr>
                <tr v-for="task in completedTasks" :key="task.id" class="completed content">
                    <td>{{ task.name }}</td>
                    <td>{{ task.description }}</td>
                    <td>
                        <button @click="task.isActive = !task.isActive">
                      {{ task.isActive ? "Mark Completed" : "Mark In Progress" }}
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>


<style scoped>

table {
    justify-content: center;
    align-items: top;
    border: 2px dashed grey;
    background-color: rgba(200,200,200, .1);
    min-width: 100%;
}

thead {
    display: flex;
    background-color: rgba(230,230, 230,.2);
}

tr {
    display: flex;
    justify-content: space-around;
    align-items: center;
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

#subHeader {
    display: flex;
    align-items: left;
}

h1 {
    text-align: center;
}



</style>