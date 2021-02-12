<template>
  <table class="workers">
      <thead>
          <tr>
              <th>Id</th>
              <th>Name</th>
              <th></th>
              <th></th>
          </tr>
      </thead>
      <tbody>
          <tr v-for="worker in workerList" :key="worker.id">
              <td>{{worker.id}}</td>
              <td>{{worker.name}}</td>
              <td @click="loadDetails(worker)" class="details" >Details</td>
              <td @click="editWorker(worker)" class="edit">Edit</td>
          </tr>
      </tbody>
  </table>
  <div v-if="projects != null && loadEdit == false" class="details-box">
      <h2>Details:</h2>
      <p>Name: {{detailsWorker.name}}</p>
      <p>Projetos:</p>
      <ul v-for="project in projects" :key="project.id">
          <li>{{project.name}} - {{project.description}} - {{project.workedHours}}</li>
      </ul>
      <button @click="deleteWorker(detailsWorker)" class="delete-button">Delete</button>
  </div>
  <div v-if="loadEdit == true">
      <h2>Edit Worker:</h2>
      <input type="text" v-model="detailsWorker.name" />
      <button @click="saveWorker">Save</button>
  </div>
</template>

<script>

export default {
    name: 'WorkerTable',
    data() {
        return {
            detailsWorker: null,
            projects: null,
            loadEdit: false,
            workerList: null
        }
    },
    created() {
        this.workerList = this.workers;
    },
    props: {
        workers: null
    },
    methods: {
        loadDetails(worker) {
            console.log(worker);
            let self = this;
            fetch('http://localhost:5000/api/worker/' + worker.id + '/project').then(function(response) {
                return response.json();
            })
            .then(function(json) {
                console.log(json);
                self.projects = json;
            });
            this.detailsWorker = worker;
        },
        deleteWorker(worker) {
            let self = this;
            fetch('http://localhost:5000/api/worker/' + worker.id, { method: 'DELETE' }).then(function(response) {
                return response.json();
            })
            .then(function(json) {
                console.log(json);
                alert("Worker deleted: " + json.name);
                self.updateWorkers();
            });
        },
        updateWorkers() {
            let self = this;
            fetch('http://localhost:5000/api/worker').then(function(response) {
            return response.json();
            })
            .then(function(json) {
                self.workerList = json;
                self.loadEdit = false;
                self.projects = null;
                self.detailsWorker = null;
            });
        },
        editWorker(worker) {
            if(this.detailsWorker == null || this.detailsWorker.id != worker.id)
            {
                this.loadEdit = true;
                this.loadDetails(worker);
            } else {
                this.loadEdit = true;
            }
        },
        saveWorker() {
            let self = this;
            let header = new Headers();
            header.append("Content-Type", "application/json");
            fetch('http://localhost:5000/api/worker/' + this.detailsWorker.id, { method: 'PUT', headers: header, body: JSON.stringify(this.detailsWorker) }).then(function(response) {
                console.log(response);
                self.updateWorkers();
            })
        }
    }
}
</script>

<style lang="scss">
.workers {
    width: 100%;
    border-spacing: 0px;
    thead {
        background-color: rgb(44, 44, 44);
        color: #fff;
        th {
            padding: 15px 5px;
        }
    }

    tbody {
        background-color: lightgray;
        
        td {
            padding: 10px 5px;
        }
    }

    .details {
        cursor: pointer;
        background-color: rgb(0, 99, 0);
        color: #fff;
    }

    .edit {
        cursor: pointer;
        background-color: rgb(201, 0, 0);
        color: #fff;
    }
}
.details-box {
    padding: 1.5rem 5rem;
    text-align: left;

    .delete-button {
        background-color: red;
        color: #fff;
        padding: 10px 25px;
        border: none;
    }
}
</style>