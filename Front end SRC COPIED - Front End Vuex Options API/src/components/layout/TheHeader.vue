<template>
        <h1>Vue gymclub 1 met Options API Vuex</h1>
    <header>
        <div>
            <nav>
            <ul>
                <li v-if="currentList != 'members' || currentList =='all'">
                    <router-link to="/members">Toon alle leden
                    </router-link>
                </li> 
                <li v-if="currentList != 'trainers' || currentList =='all'">
                    <router-link to="/trainers">Toon alle trainers</router-link>
                </li>
            </ul>
        </nav>
        </div>
    </header>
</template>

<script>
export default {
    computed: {
        currentList() {
                return this.$store.getters['currentList']; 
        },
    }
}

</script>
<style scoped>
header {
  height: 5rem;
  background-color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 10px;
}

header a {
  text-decoration: none;
  color: black;
  display: inline-block;
  padding: 0.75rem 1.5rem;
  border: 1px solid transparent;
}

a:active,
a:hover,
a.router-link-active {
  border: 1px solid #f391e3;
}

h1 {
  margin: 10px;
  background-color: #ecbcda;
  text-align: center;
}

h1 a {
  color: white;
  margin: 0;
}

h1 a:hover,
h1 a:active,
h1 a.router-link-active {
  border-color: transparent;
}

header nav {
  width: 90%;
  margin: auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

header ul {
  list-style: none;
  margin: 0;
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
}

li {
  margin: 0 0.5rem;
  color: black;
  background-color: rgb(231, 225, 225);
  border: 1px solid black;
  text-align: center;
  font-size: 20px;
  border-radius: 10px;
  box-shadow: 5px 5px grey;
}
</style>