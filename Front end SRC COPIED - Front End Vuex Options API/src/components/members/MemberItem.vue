<!-- een member item is een rij in de lijst -->
<template>
     <div v-if="deleteAlertIsVisible">
        <delete-alert 
            :title=deleteWarningTitle 
            @cancelDialog="hideDeleteAlert"
            @deleteItem="deleteMember"
        ></delete-alert>
    </div>

    <li>
        <table>
            <tr>
                <td><h5>Id: {{id}}</h5></td>
                <td><h5>PersonId: {{personId}}</h5></td>
            </tr>
            <tr>
                <td><h5>Volledige naam: {{constructFullName(sortedOn)}}</h5></td>
                <td><h5>Is ook trainer: {{convertBooleanToString(isAlsoTrainer)}}</h5></td>
            </tr>  
            <tr>
                <td><h5>Niveau Id: {{level}}</h5></td>
                <td><h5>Niveau: {{levelString}}</h5></td>
              
            </tr>
            <tr>
                <!--  <td><h5>Startdatum: {{formatDate('DDMMYYYY', startDate)}}</h5></td>  **** via mixin kan ook -->
                <td><h5>Startdatum: {{startDateInView}}</h5></td> <!-- -->
                <td><h5>Einddatum: {{endDateInView}}</h5></td>
            </tr>
        </table>
        
        <h5 class="knipperlicht" v-if="updateBackEndBusy">{{ description }}</h5>
        <div class="actions">
            <base-button link :to="memberDetailsLink">Details Lid</base-button>
            <base-button mode="rood" @click="showDeleteAlert">Verwijder lid</base-button>
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
        level: {
            type: Number,
            required: true,
        },
        startDate: {
            type: String,
            required: true,
        },
        endDate: {
            type: String,
            required: false,
        },
        isAlsoTrainer: {
            type: Boolean,
            required: false,
        },
        levelString: {
            type: String,
            required: true,
        },
        sortedOn: {
            type: String,
            required: true,
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
    emits: ['reload-members'],
    data() {
        return {
            updateBackEndBusy: false,
            deleteAlertIsVisible: false
        }
    },
    computed: {
            startDateInView() {
                return this.simplifyDateFormat(this.formatDate('DDMMYYYY', this.startDate));
            },
            endDateInView() {
                return this.endDate != '' ? this.simplifyDateFormat(this.formatDate('DDMMYYYY', this.endDate)) : ''
            },
            deleteWarningTitle() {
            const fullName = this.firstName + ' ' + this.middleName + ' ' + this.lastName;
            return fullName + ' verwijderen?';
            },
            memberDetailsLink() {
                return this.$route.path + '/' + this.id; //   /members/1
            },
            isUpdatedInBackEnd() {
                return this.$store.getters['members/isUpdatedInBackEnd']; // Zie isUpdatedInBackEnd in getters.js
            }
    },
    watch: {
        // functienaam vd watcher = functienaam van de computed property
        isUpdatedInBackEnd(newValue, oldValue) {
            if (newValue === true) {
               this.updateBackEndBusy = false;
               this.$emit('reload-members');
               this.$router.push({ path: '/members' })
            };
        }
    },
    methods: {
        showDeleteAlert() {
            this.deleteAlertIsVisible = true;
        },
        deleteMember() {
            this.deleteAlertIsVisible = false;
            this.updateBackEndBusy = true;
            this.description = `Lid \'${this.fullName}\' aan het verwijderen in back-end...`;
            this.$store.dispatch(`members/delete`, this.id); // members = namespace zie ook root index.js -- delete = local action
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