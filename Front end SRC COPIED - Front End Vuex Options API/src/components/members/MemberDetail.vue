<!-- een member item is een rij in de lijst van MembersList-->
<template>

    <div v-if="deleteAlertIsVisible">
        <delete-alert 
            :title=deleteWarningTitle 
            @cancelDialog="hidedeleteAlert"
            @deleteItem="deleteMember"
        ></delete-alert>
    </div>

    <div>
       
    <div v-if="id !=0 && !dataIsLoaded">
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
                <p><strong>Niveau: </strong>{{  level.val }}</p>  
                <p><strong>Niveau omschrijving: </strong>{{  levelString.val }}</p>
                <p><strong>Is ook trainer: </strong>{{ convertBooleanToString(isAlsoTrainer.val) }}</p> <!-- via global Mixin -->
            </div>

            <fieldset :class="updateBackEndBusy ? 'protected' : null">

                <!-- zet onderstaande tabel in een apart component -->
                <table>
                    <!-- **************************************** -->
                    <tr>
                        <td>
                            <div class="form-control" :class="{invalid: !firstName.isValid}">
                                <label for="firstName">Voornaam</label>
                                <input tabindex = 1 type="text" id="firstName" v-model.trim="firstName.val" @blur="clearValidity('firstName')"/>
                                <p class="errorMessage" v-if="!firstName.isValid">{{ errorString }}</p>
                            </div>
                        </td>
                        <td>
                            <div class="form-control" :class="{invalid: !startDate.isValid}">
                                <label for="startDate">Startdatum</label>
                                <input tabindex = 4 type="text" id="startDate" v-model.trim="startDate.val" @blur="clearValidity('startDate')"/>
                                <p class="errorMessage" v-if="!startDate.isValid">{{ errorString }}</p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-control" :class="{invalid: !middleName.isValid}">
                                <label for="middleName">Tussenvoegsels</label>
                                <input tabindex = 2 type="text" id="middleName" v-model.trim="middleName.val"/>
                            </div>
                        </td>
                        <td>
                            <div class="form-control" :class="{invalid: !endDate.isValid}">
                                <label for="endDate">Einddatum (mag leeg zijn)</label>
                                <input tabindex = 5 type="text" id="endDate" v-model.trim="endDate.val" @blur="clearValidity('endDate')"/>
                                <p class="errorMessage" v-if="!endDate.isValid">{{ errorString }}</p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-control" :class="{invalid: !lastName.isValid}">
                                <label for="lastName">Achternaam</label>
                                <input tabindex = 3 type="text" id="lastName" v-model.trim="lastName.val" @blur="clearValidity('lastName')"/>
                                <p class="errorMessage" v-if="!lastName.isValid">{{ errorString }}</p>
                            </div>
                        </td>
                    </tr>
                </table>

            <hr>

                <table>
                    <tr>
                        <td>Is ook trainer</td>
                        <input id = "isAlsoTrainer" type="checkbox" v-model="isAlsoTrainer.val">
                    </tr>
                </table>

                <div class="container">
                    <!-- dynamisch maken van het aantal radio buttons is moeilijk, lijkt me ook geen common practice -->
                    <label for=1 style="float: left; margin: 10px">Beginner</label>
                        <input type="radio" id=1 value=1 v-model="level.val">
                    <br>
                    <label for=2 style="float: left; margin: 10px">Intermediate</label>
                        <input type="radio" id=2 value=2 v-model="level.val">
                    <br>
                    <label for=3 style="float: left; margin: 10px">Experienced</label>
                        <input type="radio" id=3 value=3 v-model="level.val">
                    <br>
                </div>

            </fieldset>
            <base-button @click="submitForm">Verstuur</base-button>
            <base-button v-if="this.id != 0" @click="showdeleteAlert">Verwijderen</base-button>    
            <div>
                <h4 v-if="showGeneralError" class="errorMessage">{{ generalErrorText }}</h4>
            </div>
            <div v-if="updateBackEndBusy">
                <busy-loading
                    :description="description"
                ></busy-loading>
            </div>
        </form>
    </div>
    </div>

</template>

<script>
import formatterMixins from '../../generalMixins/formatters/formatters.js';
import validatorMixins from '../../generalMixins/validators/validators.js';
import memberDetailMixins from './mixins/memberDetailMixins.js';
import deleteAlert from '../ui/DeleteAlert.vue';
export default {
    components: { // sub componenten kan je NIET in een mixin.js file zetten.
        deleteAlert
    },
    mixins: [memberDetailMixins, formatterMixins, validatorMixins],
    // props(), data(), computed() zitten in de mixin
    // Je kan niet klakkeloos willekeurige items in mixins zetten, ze hebben toch een bepaalde afhankelijkheid van elkaar
    watch: {
        // functienaam vd watcher = functienaam van de computed property = isLoaded
        isLoaded(newValue, oldValue) {
            if (newValue === true) {
                let endDate = this.member.endDate ? this.formatDate('DDMMYYYY', this.member.endDate) : '';
                if (endDate != '') {
                    endDate = this.simplifyDateFormat(endDate);
                }

                this.dataIsLoaded = true;
                this.firstName.val = this.member.person.firstName;
                this.middleName.val = this.member.person.middleName;
                this.lastName.val = this.member.person.lastName;
                this.level.val = this.member.level;

                this.startDate.val = this.simplifyDateFormat(this.formatDate('DDMMYYYY', this.member.startDate));
                this.endDate.val = endDate;

                this.isAlsoTrainer.val = this.member.isAlsoTrainer;
                this.levelString.val = this.member.levelString;
                this.personId = this.member.personId;
            };
        },
        // functienaam vd watcher = functienaam van de computed property
        isUpdatedInBackEnd(newValue, oldValue) {
            if (newValue === true) {
               this.updateBackEndBusy = false;
               if (!this.errorFromBackEnd) {
                   this.$router.push({ path: '/members' })
               } else {
                    this.showGeneralError = true;
               }
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
        deleteMember() {
            this.deleteAlertIsVisible = false;
            this.updateBackEndBusy = true;
            const fullName = this.firstName.val + ' ' + this.middleName.val + ' ' + this.lastName.val;
            this.description = `Lid \'${fullName}\' aan het verwijderen in back-end...`;
            this.$store.dispatch(`members/delete`, this.id); // members = namespace zie ook root index.js -- delete = local action
        },
        get(id) {
            this.description =`Aan het inladen: Lid detail id=${this.id}...`,
            // Backend slaapt bewust 2 seconden
            this.$store.dispatch(`members/get`, {id}); // members = namespace zie ook root index.js -- get = local action
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

            const endDate = this.endDate.val != '' ? this.formatDate('YYYYMMDD', this.endDate.val) : ''; 
            const formData = {
                id: +this.id,
                level: +this.level.val,
                startDate: this.formatDate('YYYYMMDD', this.startDate.val),
                endDate,
                isAlsoTrainer: false,
                isAlsoTrainer: this.isAlsoTrainer.val,
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
                this.description = `Lid \'${fullName}\' aan het Wijzigen in back-end...`;
                this.$store.dispatch(`members/update`, formData); // members = namespace zie ook root index.js -- update = local action

            } else {
                this.description = `Lid \'${fullName}\' aan het toevoegen in back-end...`;
                this.$store.dispatch(`members/add`, formData); // members = namespace zie ook root index.js -- add = local action
            }
        },
        validateForm() {
            this.formIsValid = true;
            this.showGeneralError = false;

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

            // Convert d-m-yyyy to dd-mm-yyyy etc with extra preceding zeroes
            this.startDate.val = this.makeDateFormatComplete(this.startDate.val);
            const validationResult = this.isValidDate('DDMMYYYY', this.startDate.val); // via een mixin
            if (!validationResult.ok) { 
                this.startDate.isValid = false;
                this.errorString = getErrorString('E04', this.errorCodes) + validationResult.errorDetails;
                this.formIsValid = false;
                return;
            };

            if (this.endDate.val != '') { 
                this.endDate.val = this.makeDateFormatComplete(this.endDate.val);
                const validationResult = this.isValidDate('DDMMYYYY', this.endDate.val); // via een mixin
                if (!validationResult.ok) {
                    this.endDate.isValid = false;
                    this.errorString = getErrorString('E04', this.errorCodes) + validationResult.errorDetails;
                    this.formIsValid = false;
                return;
                }
            };

            // Controleer of einddatum > begindatum
            if (!this.check2DatesValid(this.startDate.val, this.endDate.val)) {
                this.showGeneralError = true;
                this.$store.dispatch('members/setGeneralErrorText', 'Begindatum is groter dan einddatum');
                this.formIsValid = false;
                return;
            }
        },
       
    },
    // created() lifecycle hook: zou ook via mixin kunnen
    created() {
        if (this.id != 0) {
            this.get(this.id);
        } else {
            this.level.val = 1;
        }
        this.$store.dispatch('setCurrentList', 'all');
        this.$store.dispatch('members/setGeneralErrorText', '');
    } 
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