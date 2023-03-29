<template>
    <h2>Trainers lijst, gesorteerd op {{ sortedOnDescription }}</h2>

    <div v-if="!dataIsLoaded">
        <busy-loading
            :description="description">
        </busy-loading>
    </div>

    <div v-else>
        <div class="actions">
            <div>
                <base-button mode="groen" link :to="trainerAddLink">Toevoegen trainer</base-button>
            </div>
            <!-- Herstel alle trainers doen we voor het gemak niet, vanwege complexiteit in herstellen IsAlsoTrainer bij de members -->
            <!-- <div>
                <base-button mode="groen" @click="recoverDeletedTrainers"><i>Herstellen alle verwijderde trainers</i></base-button>
            </div> -->
            <base-button 
                v-if="sortedOn != 'id'"
                mode="oranje" 
                @click="sortListOnId">
                Sorteren op Id
            </base-button>
            <base-button 
                v-if="sortedOn != 'firstName'" 
                mode="oranje" 
                @click="sortListOnFirstName">
                Sorteren op voornaam
            </base-button>
            <base-button 
                v-if="sortedOn != 'lastName'"
                mode="oranje" 
                @click="sortListOnLastName">
                Sorteren op achternaam
            </base-button>
        </div>
        <ul>
            <trainer-item v-for="trainer in trainers" 
                        @reload-trainers="reloadTrainers"
                        :key="trainer.id" 
                        :id="trainer.id"
                        :person-id="trainer.personId"
                        :is-certified="trainer.isCertified"
                        :sorted-on="sortedOn"
                        :first-name="trainer.person.firstName"
                        :middle-name="trainer.person.middleName"
                        :last-name="trainer.person.lastName"
                    >
            </trainer-item>
        </ul>
    </div>
    
</template>

<script>
import TrainerItem from './TrainerItem.vue';
// import leden from '../../dummyData/trainersAndTrainers.js'
import trainersListMixins from './mixins/trainersListMixins.js';

export default {
    components: { // sub componenten kan je NIET in een mixin.js file zetten.
        TrainerItem
    },
    // data() en computed() vervangen door mixIn
    mixins: [trainersListMixins],
    
    watch: {
        // functienaam vd watcher = functienaam van de computed property
        isLoaded(newValue, oldValue) {
            if (newValue === true) {
                this.dataIsLoaded = true;
            };
        },
    },
    methods: {
        reloadTrainers() {
            this.getList();
        },
        getList() {
            this.dataIsLoaded = false;
            this.description = 'Inladen trainerslijst...';
            this.$store.dispatch('trainers/getList'); 
        },
        sortListOnFirstName() {
            this.trainers.sort((a, b) => a.person.firstName.localeCompare(b.person.firstName));
            this.sortedOn='firstName';
        },
        sortListOnLastName() {
            this.trainers.sort((a, b) => a.person.lastName.localeCompare(b.person.lastName));
            this.sortedOn='lastName';
        },
        sortListOnId() {
            this.trainers.sort((a, b) => a.id - b.id);
            this.sortedOn='id';
        }
    },
    created() {
        this.sortedOn='id';
        this.$store.dispatch('setCurrentList', 'trainers');
        this.getList();
    }
}
</script>

<style scoped>
ul {
    list-style-type:none /* uitschakelen bullets */
}

h2 {
    margin-left: 20px;
}

</style>