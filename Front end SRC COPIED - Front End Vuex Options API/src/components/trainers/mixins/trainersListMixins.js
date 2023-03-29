export default {
    data() {
        return {
            dataIsLoaded: false,
            description: '',
            sortedOn: 'id' //NIET op leeg zetten. Mogelijke waarden: id / firstName / lastName
        }
    },
    computed: {
        trainers() {
            // 'trainers' is namespace, zie root index.js en 'trainers' is de getter zelf binnen trainers/getters.js
            return this.$store.getters['trainers/trainers']; 
        },
        sortedOnDescription() {
            switch(this.sortedOn) {
                case 'id': return 'id';
                case 'firstName': return 'Voornaam';
                case 'lastName': return 'Achternaam';
            } 
        },
        trainerAddLink() {
            return this.$route.path + '/0'; 
        },
        isLoaded() {
            return this.$store.getters['trainers/isLoaded'];
        },
    },
}