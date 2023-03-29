import errorCodes from '../../../errorCodes/errorCodes.js'; // deze MOET hier

export default {
    props: { // props is niet verplicht in deze mixin
        id: {
            type: String, // Number geeft een warning, kan ik niet oplossen
            required: true
        }
    },
    data() {
        return {
            picked: null,
            firstName: {
                val: '',
                isValid: true
            },
            lastName: {
                val: '',
                isValid: true
            },
            middleName: {
                val: '',
                isValid: true
            },
            isCertified: {
                val: false,
                isValid: true
            },
            personId: 0,
            formIsValid: true,
            errorString: '',
            errorCodes: errorCodes,
            dataIsLoaded: false,
            updateBackEndBusy: false,
            description: '',
            deleteAlertIsVisible: false
        }
    },
    computed: {
        trainer() {
            // 'trainers' is namespace, zie root index.js en 'trainer' is de getter zelf binnen trainers/getters.js
                return this.$store.getters['trainers/trainer']; 
        },
        fullName() {
            return this.trainer.person.firstName + ' ' + this.trainer.person.middleName + ' ' + this.trainer.person.lastName;
        },
        deleteWarningTitle() {
            const fullName = this.trainer.person.firstName + ' ' + this.trainer.person.middleName + ' ' + this.trainer.person.lastName;
            return fullName + ' verwijderen?';
        },
        isLoaded() {
            if (this.id) {
                return this.$store.getters['trainers/isLoaded'];
            } else {
                return false; // nvt
            }
        },
        isUpdatedInBackEnd() {
                return this.$store.getters['trainers/isUpdatedInBackEnd']; // Zie isUpdatedInBackEnd in getters.js
        }
    },
    created() {
        if (this.id != 0) {
            this.get(this.id);
        } else {
            this.isCertified.val = 0;
        }
        this.$store.dispatch('setCurrentList', 'all');
    } 
}

