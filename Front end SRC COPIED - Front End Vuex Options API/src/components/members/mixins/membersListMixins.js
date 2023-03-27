export default {
    data() {
        return {
            dataIsLoaded: false,
            dataBackEndIsRecovered: false,
            description: 'Inladen ledenlijst...',
            sortedOn: 'id' //NIET op leeg zetten. Mogelijke waarden: id / firstName / lastName
        }
    },
    computed: {
        members() {
            // 'members' is namespace, zie root index.js en 'members' is de getter zelf binnen members/getters.js
            return this.$store.getters['members/members']; 
        },
        sortedOnDescription() {
            switch(this.sortedOn) {
                case 'id': return 'id';
                case 'firstName': return 'Voornaam';
                case 'lastName': return 'Achternaam';
            } 
        },
        memberAddLink() {
            return this.$route.path + '/0'; 
        },
        isLoaded() {
            return this.$store.getters['members/isLoaded'];
        },
        recoverBackEndIsBusy() { // Deze gebruiken we voor Recover all deleted (parameter 'members' of 'trainers'
            return this.$store.getters['recoverBackEndIsBusy']; // Zie main store --> getters.js
    }
    },
}