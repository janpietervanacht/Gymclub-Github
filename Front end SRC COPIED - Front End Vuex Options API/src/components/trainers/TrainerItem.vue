<!-- een trainer item is een rij in de lijst -->
<template>
     <div v-if="deleteAlertIsVisible">
        <delete-alert 
            :title=deleteWarningTitle 
            @cancelDialog="hideDeleteAlert"
            @deleteItem="deleteTrainer"
        ></delete-alert>
    </div>

    <li>
        <table>
            <tr>
                <td><h5>Id: {{id}}</h5></td>
                <td><h5>PersonId: {{personId}}</h5></td>
            </tr>
            <tr>
                <td><h5>Volledige naam: {{fullName}}</h5></td>
                <td><h5>Is gecertificeerd: {{convertBooleanToString(isCertified)}}</h5></td>
            </tr>  
        </table>
        
        <h5 class="knipperlicht" v-if="updateBackEndBusy">{{ description }}</h5>
        <div class="actions">
            <base-button link :to="trainerDetailsLink">Details trainer</base-button>
            <base-button mode="rood" @click="showDeleteAlert">Verwijder trainer</base-button>
        </div>
    </li>
</template>

<script>
import formatterMixins from '../../generalMixins/formatters/formatters.js';
import DeleteAlert from '../ui/DeleteAlert.vue';
export default {
    components: { 
        DeleteAlert
    },
    // props: ['id', 'level', 'levelString' 'firstName', 'middleName', 'lastName'] // mag ook
    props: {
        id: {
            type: Number,
            required: true
        },
        personId: {
            type: Number,
            required: true
        },
        isCertified: {
            type: Boolean,
            required: false,
        },
        sortedOn: {
            type: String,
            required: true 
        },
        firstName: {
            type: String,
            required: true 
        },
        middleName: {
            type: String,
            required: false,
            default: '' 
        },
        lastName: {
            type: String,
            required: true 
        },
    },
    mixins: [formatterMixins],
    emits: ['reload-trainers'],
    data() {
        return {
            updateBackEndBusy: false,
            deleteAlertIsVisible: false
        }
    },
    computed: {
            fullName() {
                if (this.sortedOn == 'lastName') {
                    return this.lastName + ', ' + this.firstName + ' ' + this.middleName;
                } else {
                    return this.firstName + ' ' + this.middleName + ' ' + this.lastName;
                }
            },
            deleteWarningTitle() {
            const fullName = this.firstName + ' ' + this.middleName + ' ' + this.lastName;
            return fullName + ' verwijderen?';
            },
            trainerDetailsLink() {
                return this.$route.path + '/' + this.id; //   /trainers/1
            },
            isUpdatedInBackEnd() {
                return this.$store.getters['trainers/isUpdatedInBackEnd']; // Zie isUpdatedInBackEnd in getters.js
            }
    },
    watch: {
        // functienaam vd watcher = functienaam van de computed property
        isUpdatedInBackEnd(newValue, oldValue) {
            if (newValue === true) {
               this.updateBackEndBusy = false;
               this.$emit('reload-trainers');
               this.$router.push({ path: '/trainers' })
            };
        }
    },
    methods: {
        showDeleteAlert() {
            this.deleteAlertIsVisible = true;
        },
        deleteTrainer() {
            this.deleteAlertIsVisible = false;
            this.updateBackEndBusy = true;
            this.description = `Trainer \'${this.fullName}\' aan het verwijderen in back-end...`;
            this.$store.dispatch(`trainers/delete`, this.id); // trainers = namespace zie ook root index.js -- delete = local action
        },
        hideDeleteAlert() {
            this.deleteAlertIsVisible = false;
        },
    }
}
</script>

<style scoped>
td {
    padding-right: 100px;
}

input[type="checkbox"]:checked {
  background: blue;
  color: white;
}
.knipperlicht {
    margin-left: 20px;
    color: red;
    font-style: italic;
    animation: blink-animation 1s steps(10, start) infinite;
        -webkit-animation: blink-animation 1s steps(10, start) infinite;
      }
@keyframes blink-animation {
    to {
        visibility: hidden;
    }
}
@-webkit-keyframes blink-animation {
    to {
        visibility: hidden;
    }
}

li {
  margin: 20px;
  border: 1px solid #1d20af;
  border-radius: 12px;
  padding: 20px;
}

h3 {
  font-size: 1.5rem;
}

h3,
h4 {
  margin: 0.5rem 0;
}

div {
  margin: 0.5rem 0;
}

.actions {
  display: flex;
  justify-content: flex-end;
}
</style>