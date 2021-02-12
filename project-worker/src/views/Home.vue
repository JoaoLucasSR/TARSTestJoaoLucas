<template>
  <div class="home">
    <div>
      <p>Add new Worker: </p>
      <input type="text" v-model="newWorker.name" placeholder="Name" />
      <button @click="createWorker">Add</button>
    </div>
    <br/>
    <p v-if="loading">Loading...</p>
    <div class="table-div" v-else>
      <WorkerTable :workers="workers" />
    </div>
  </div>
</template>

<script>
import WorkerTable from "../components/WorkerTable";
export default {
  name: 'Home',
  data() {
    return {
      loading: true,
      workers: null,
      newWorker: { name: "" }
    }
  },
  components: {
    WorkerTable
  },
  created() {
    this.updateWorkers();
  },
  methods: {
    createWorker() {
      let header = new Headers();
      header.append("Content-Type", "application/json");
      this.loading = true;
      let self = this;
      if(this.newWorker.name != null) {
        fetch('http://localhost:5000/api/worker', {method: 'POST', headers: header, body: JSON.stringify(this.newWorker)}).then(function(response) {
          console.log(response);
          self.updateWorkers();
          self.newWorker.name = "";
        });
      }
    },
    updateWorkers() {
      let self = this;
      fetch('http://localhost:5000/api/worker').then(function(response) {
        console.log(response);
        return response.json();
      })
      .then(function(json) {
        self.workers = json;
        self.loading = false;
      });
    }
  }
}
</script>

<style lang="scss">
.table-div {
  display: flex;
  flex: 1;
  flex-direction: column;
}
</style>
