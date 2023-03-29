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
            level: {
                val: 0,
                isValid: true
            },
            levelString: {
                val: '',
                isValid: true
            },
            startDate: {
                val: '',
                isValid: true
            },
            endDate: {
                val: '',
                isValid: true
            },
            isAlsoTrainer: {
                val: false,
                isValid: true
            },
            personId: 0,
            formIsValid: true,
            errorString: '',
            errorCodes: errorCodes,
            showGeneralError: false,
            dataIsLoaded: false,
            updateBackEndBusy: false,
            description: '',
            deleteAlertIsVisible: false,
        }
    },
    computed: {
        generalErrorText() {
            return this.$store.getters['members/generalErrorText'];
        },
        errorFromBackEnd() {
            return this.$store.getters['members/errorFromBackEnd'];
        },
        member() {
            // 'members' is namespace, zie root index.js en 'member' is de getter zelf binnen members/getters.js
                return this.$store.getters['members/member']; 
        },
        fullName() {
            return this.member.person.firstName + ' ' + this.member.person.middleName + ' ' + this.member.person.lastName;
        },
        deleteWarningTitle() {
            const fullName = this.member.person.firstName + ' ' + this.member.person.middleName + ' ' + this.member.person.lastName;
            return fullName + ' verwijderen?';
        },
        isLoaded() {
            if (this.id) {
                return this.$store.getters['members/isLoaded'];
            } else {
                return false; // nvt
            }
        },
        isUpdatedInBackEnd() {
                return this.$store.getters['members/isUpdatedInBackEnd']; // Zie isUpdatedInBackEnd in getters.js
        }
    },
   
}

