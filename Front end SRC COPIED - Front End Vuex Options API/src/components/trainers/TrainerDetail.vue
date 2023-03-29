<!-- een trainer item is een rij in de lijst van TrainersList-->
<template>

    <div v-if="deleteAlertIsVisible">
        <delete-alert 
            :title=deleteWarningTitle 
            @cancelDialog="hidedeleteAlert"
            @deleteItem="deleteTrainer"
        ></delete-alert>
    </div>

    <div>
        <div v-if="(id !=0 && !dataIsLoaded) || (updateBackEndBusy)">
        <busy-loading
            :description="description"
        ></busy-loading>
    </div>

    <div v-if="(dataIsLoaded || id == 0)">
        <h4 v-if="id != 0">Id: {{id}}</h4>
        <h4 v-if="id != 0">PersonId: {{personId}}</h4>

        <!-- <form @submit.prevent="submitForm"> -->
        <form>
            <div v-if="dataIsLoaded">
                <p ><strong>Volledige naam: </strong>{{  fullName }}</p>
                <p><strong>Gecertificeerd</strong>{{  convertBooleanToString(isCertified.val) }}</p> <!-- via Mixin -->
            </div>

            <fieldset :class="updateBackEndBusy ? 'protected' : null">

                <table>
                    <tr>
                        <td>
                            <div class="form-control" :class="{invalid: !firstName.isValid}">
                                <label for="firstName">Voornaam</label>
                                <input type="text" id="firstName" v-model.trim="firstName.val" @blur="clearValidity('firstName')"/>
                                <p class="errorMessage" v-if="!firstName.isValid">{{ errorString }}</p>
                            </div>
                        </td>
                        <td>
                            <div class="form-control" :class="{invalid: !middleName.isValid}">
                                <label for="middleName">Tussenvoegsels</label>
                                <input type="text" id="middleName" v-model.trim="middleName.val"/>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-control" :class="{invalid: !lastName.isValid}">
                                <label for="lastName">Achternaam</label>
                                <input type="text" id="lastName" v-model.trim="lastName.val" @blur="clearValidity('lastName')"/>
                                <p class="errorMessage" v-if="!lastName.isValid">{{ errorString }}</p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                        </td>
                    </tr>
                </table>

            <hr>

                <table>
                    <tr>
                        <td>Is gecertificeerd</td>
                        <input id = "isCertified" type="checkbox" v-model="isCertified.val">
                    </tr>
                </table>

            </fieldset>
            <base-button @click="submitForm">Verstuur</base-button>
            <base-button v-if="this.id != 0" @click="showdeleteAlert">Verwijderen</base-button>    

        </form>
    </div>
    </div>

</template>

<script>
import formatterMixins from '../../generalMixins/formatters/formatters.js';
import trainerDetailMixins from './mixins/trainerDetailMixins.js';
import deleteAlert from '../ui/DeleteAlert.vue';
export default {
    components: { // sub componenten kan je NIET in een mixin.js file zetten.
        deleteAlert
    },
    mixins: [trainerDetailMixins, formatterMixins],
    // props(), data(), computed() en created() zitten in de mixin
    // Je kan niet klakkeloos willekeurige items in mixins zetten, ze hebben toch een bepaalde afhankelijkheid van elkaar
    watch: {
        // functienaam vd watcher = functienaam van de computed property = isLoaded
        isLoaded(newValue, oldValue) {
            if (newValue === true) {
                this.dataIsLoaded = true;
                this.firstName.val = this.trainer.person.firstName;
                this.middleName.val = this.trainer.person.middleName;
                this.lastName.val = this.trainer.person.lastName;
                this.isCertified.val = this.trainer.isCertified;
                this.personId = this.trainer.personId;
            };
        },
        // functienaam vd watcher = functienaam van de computed property
        isUpdatedInBackEnd(newValue, oldValue) {
            if (newValue === true) {
               this.updateBackEndBusy = false;
               this.$router.push({ path: '/trainers' })
            };
        }
    },
    methods: {
        showdeleteAlert() {
            this.deleteAlertIsVisible = true;
        },
        hidedeleteAlert() {
            this.deleteAlertIsVisible = false;
        },
        deleteTrainer() {
            this.deleteAlertIsVisible = false;
            this.updateBackEndBusy = true;
            const fullName = this.firstName.val + ' ' + this.middleName.val + ' ' + this.lastName.val;
            this.description = `Trainer \'${fullName}\' aan het verwijderen in back-end...`;
            this.$store.dispatch(`trainers/delete`, this.id); // trainers = namespace zie ook root index.js -- delete = local action
        },
        get(id) {
            this.description =`Aan het inladen: Trainer detail id=${this.id}...`,
            // Backend slaapt bewust 2 seconden
            this.$store.dispatch(`trainers/get`, {id}); // trainers = namespace zie ook root index.js -- get = local action
        },
        clearValidity(input) {
            this[input].isValid = true;
        },
        submitForm() {
            this.validateForm();
            if (!this.formIsValid) {
                return;
            }
            this.updateBackEndBusy = true;

            const formData = {
                id: +this.id,
                isCertified: this.isCertified.val,
                personId: this.personId,
                person: {
                    id: this.personId,
                    firstName: this.firstName.val,                    
                    lastName: this.lastName.val,                    
                    middleName: this.middleName.val,                    
                }
            };

            const fullName = this.firstName.val + ' ' + this.middleName.val + ' ' + this.lastName.val;
            if (this.id != 0) {
                this.description = `Trainer \'${fullName}\' aan het Wijzigen in back-end...`;
                this.$store.dispatch(`trainers/update`, formData); // trainers = namespace zie ook root index.js -- update = local action

            } else {
                this.description = `Trainer \'${fullName}\' aan het toevoegen in back-end...`;
                this.$store.dispatch(`trainers/add`, formData); // trainers = namespace zie ook root index.js -- add = local action
            }
        },
        validateForm() {
            this.formIsValid = true;

            this.errorString = '';
            if (this.firstName.val === '') {
                this.firstName.isValid = false;
                this.errorString = getErrorString('E01', this.errorCodes);
                this.formIsValid = false;
                return;
            };
            if (this.lastName.val === '') {
                this.lastName.isValid = false;
                this.errorString = getErrorString('E02', this.errorCodes);
                this.formIsValid = false;
                return;
            };
        },
    },
    // created() lifecycle hook: via mixin
}

function getErrorString(errorCode, errorCodes)  {
    // data properties met 'this.'' ervoor kan je hier niet benaderen
    const idx = errorCodes.codes.indexOf(errorCode);
    return errorCodes.descriptions[idx];
}

</script>

<style scoped>

.protected input {
    pointer-events:none; /* Dit is een truc om een hele form te disablen */
    background-color: lightgrey;
}

.protected label {
    pointer-events:none; /* Dit is een truc om een hele form te disablen */
    background-color: lightgrey;
}

.errorMessage {
    font-size: 15px;
    font-style: italic;
    color: rgb(243, 11, 11);
}
.form-control {
  margin: 0.5rem 0;
}

label {
  font-weight: bold;
  display: block;
  margin-bottom: 0.5rem;
}

input[type='checkbox'] + label {
  font-weight: normal;
  display: inline;
  margin: 0 0 0 0.5rem;
}

input,
textarea {
  margin: 20px;
  display: block;
  width: 50%;
  border: 1px solid #ccc;
  font: inherit;
}

input:focus,
textarea:focus {
  background-color: #f0e6fd;
  outline: none;
  border-color: #3d008d;
}

input[type='checkbox'] {
  display: inline;
  width: auto;
  border: none;
}

input[type='checkbox']:focus {
  outline: #3d008d solid 1px;
}

h3 {
  margin: 0.5rem 0;
  font-size: 1rem;
}

label, h4 {
  margin: 20px;
}

.invalid label {
  color: red;
}

.invalid input,
.invalid textarea {
  border: 1px solid red;
}

p {
    margin: 10px 10px 10px 20px;
}
</style>