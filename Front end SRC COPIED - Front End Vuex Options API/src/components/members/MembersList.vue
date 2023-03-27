<template>
    <h2>Members List, gesorteerd op {{ sortedOnDescription }}</h2>

    <div v-if="!dataIsLoaded || recoverBackEndIsBusy">
        <busy-loading
            :description="description">
        </busy-loading>
    </div>

    <div v-else>
        <div class="actions">
            <div>
                <base-button mode="groen" link :to="memberAddLink">Toevoegen lid</base-button>
            </div>
            <div>
                <base-button mode="groen" @click="recoverDeletedMembers"><i>Herstellen alle verwijderde leden</i></base-button>
            </div>
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
            <member-item v-for="mbr in members" 
                        @reload-members="reloadMembers"
                        :key="mbr.id" 
                        :id="mbr.id"
                        :person-id="mbr.personId"
                        :level="mbr.level"
                        :level-string="mbr.levelString"
                        :start-date="mbr.startDate"
                        :end-date="mbr.endDate"
                        :is-also-trainer="mbr.isAlsoTrainer"
                        :first-name="mbr.person.firstName"
                        :middle-name="mbr.person.middleName"
                        :last-name="mbr.person.lastName"
                    >
            </member-item>
        </ul>
    </div>
    
</template>

<script>
import MemberItem from './MemberItem.vue';
// import leden from '../../dummyData/membersAndTrainers.js'
import membersListMixins from './mixins/membersListMixins.js';

export default {
    components: { // sub componenten kan je NIET in een mixin.js file zetten.
        MemberItem
    },
    // data() en computed() vervangen door mixIn
    mixins: [membersListMixins],
    
    watch: {
        // functienaam vd watcher = functienaam van de computed property
        isLoaded(newValue, oldValue) {
            if (newValue === true) {
                this.dataIsLoaded = true;
            };
        },
        recoverBackEndIsBusy(newValue, oldValue) {
            if (newValue === false) {
                this.getList();
            };
        }
    },
    methods: {
        reloadMembers() {
            this.getList();
        },
        getList() {
            // Backend slaapt bewust 4 seconden
            this.dataIsLoaded = false;
            this.description = 'Inladen ledenlijst...';
            this.$store.dispatch('members/getList'); // members = namespace zie ook root index.js -- getList = local action
        },
        recoverDeletedMembers() {
            // stuur signaal naar back end voor stored procedure
            this.dataBackEndIsRecovered = false;
            this.description = 'Ledenlijst aan het herstellen...';
            this.$store.dispatch('recover', 'member'); // geen namespace, dus via de main store (index.js) voor back end controller = 'member'
        },
        sortListOnFirstName() {
            // console.log('Vooraf: ' + this.members);  // werkt NIET, je kan geen object aan een string appenden
            // console.log('Vooraf: ', this.members);  // werkt wel
            this.members.sort((a, b) => a.person.firstName.localeCompare(b.person.firstName));
            this.sortedOn='firstName';
        },
        sortListOnLastName() {
            this.members.sort((a, b) => a.person.lastName.localeCompare(b.person.lastName));
            this.sortedOn='lastName';
        },
        sortListOnId() {
            this.members.sort((a, b) => a.id - b.id);
            this.sortedOn='lastName';
        }
    },
    created() {
        this.sortedOn='id';
        this.$store.dispatch('setCurrentList', 'members');
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