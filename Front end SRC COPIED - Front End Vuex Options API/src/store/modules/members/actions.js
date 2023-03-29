import memberLevelEnum from '../../../enums/memberLevel.js';
import urls from '../../../constants/urls.js'

/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

/* Coach related state, mutations, actions, getters */
export default {

    setGeneralErrorText(context, payload) {
        context.commit('setGeneralErrorText', payload); // zie local 'mutations': zet members in local state
    },

    //------------------------------------------------------- 

    async getList(context, payload) { // een action wordt altijd 'dispatched' (hier is dat in MembersList.Vue)
       
        context.commit('setIsLoaded', false); 
        // Haal de URL op uit het SWAGGER scherm 'Curl'
        // Case is onbelangrijk voor de route
        const response = await fetch(`${urls.memberUrl}/GETList`);  // member = controller naam - krijgt Curl nummer = 7219
        const responseData = await response.json();
        context.commit('setIsLoaded', true); 

        if (!response.ok) {
           const error = new Error(responseData.message || 'Failed to fetch!');
           throw error;
        }

        const members = [];
        for (const key in responseData) {

            const idxLevel = memberLevelEnum.values.indexOf(responseData[key].level);
            const endDate = responseData[key].endDate? responseData[key].endDate.substring(0,10) : '';

            const member = {
                id: responseData[key].id,
                personId: responseData[key].personId,
                person: {
                    personId: responseData[key].person.id,
                    firstName: responseData[key].person.firstName,
                    middleName: responseData[key].person.middleName,
                    lastName: responseData[key].person.lastName,
                },
                // level: responseData[key].level,
                level: responseData[key].level,
                levelString: memberLevelEnum.mapping[idxLevel],
                startDate: responseData[key].startDate.substring(0,10),
                endDate,
                isAlsoTrainer: responseData[key].isAlsoTrainer,
            };
            members.push(member);
        };

        context.commit('setMembers', members); // zie local 'mutations': zet members in local state
    },

    //------------------------------------------------------- 

    async get(context, payload) { // een action wordt altijd 'dispatched' (hier is dat in MembersList.Vue)

        context.commit('setIsLoaded', false); 
        // Case is onbelangrijk voor de route
        const response = await fetch(urls.memberUrl + '/' + payload.id); // Haal de URL op uit het SWAGGER scherm 'Curl'
        const responseData = await response.json();
        context.commit('setIsLoaded', true); 

        const idxLevel = memberLevelEnum.values.indexOf(responseData.level);
        const endDate = responseData.endDate? responseData.endDate.substring(0,10) : '';
        
        const member = {
            id: responseData.id,
            personId: responseData.personId,
            person: {
                id: responseData.person.id,
                firstName: responseData.person.firstName,
                middleName: responseData.person.middleName,
                lastName: responseData.person.lastName,
            },
            level: responseData.level,
            levelString: memberLevelEnum.mapping[idxLevel],
            startDate: responseData.startDate.substring(0,10),
            endDate,
            isAlsoTrainer: responseData.isAlsoTrainer
        };
        context.commit('setMember', member); // zie local 'mutations': zet members in local state
    },

    //------------------------------------------------------- 

    async delete(context, payload) {
        context.commit('isUpdatedInBackEnd', {
            isUpdatedInBackEnd: false,
            errorFromBackEnd: false,
            errorText: null
        }); 
        const response = await fetch(urls.memberUrl + '/' + payload, {
            method: 'DELETE', // data is overriden OR added by Firebase
            headers: {
                'Content-Type': 'application/json'
              },
            // body: JSON.stringify(member)
        });
        context.commit('isUpdatedInBackEnd', {
            isUpdatedInBackEnd: true,
            errorFromBackEnd: false,
            errorText: null
        }); 
    },

    //------------------------------------------------------- 

    async add(context, payload) {
        const member = {
            id: 0,
            personId: 0,
            person: {
                id: 0,
                firstName: payload.person.firstName,
                middleName: payload.person.middleName,
                lastName: payload.person.lastName,
            },
            level: payload.level,
            startDate: payload.startDate,
            endDate: payload.endDate != '' ? payload.endDate : null,
            isAlsoTrainer: payload.isAlsoTrainer
        };

        context.commit('isUpdatedInBackEnd', {
            isUpdatedInBackEnd: false,
            errorFromBackEnd: false,
            errorText: null
        }); 
        
        const response = await fetch(urls.memberUrl, {
            method: 'POST', // data is overriden OR added by Firebase
            headers: {
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(member)
        });
        
        const resultMessage = await response.json();
        let errorFromBackEnd = false;
        let errorText = '';
        if (!resultMessage.ok) {
            context.commit('setGeneralErrorText', resultMessage.errorText);
            errorFromBackEnd = true;
            errorText = resultMessage.errorText;
        } 
        context.commit('isUpdatedInBackEnd', {
            isUpdatedInBackEnd: true,
            errorFromBackEnd,
            errorText
        }); 
    },


    async update(context, payload) {
        context.commit('setGeneralErrorText', '');

        const member = {
            id: payload.id,
            personId: payload.personId,
            person: {
                id: payload.person.id,
                firstName: payload.person.firstName,
                middleName: payload.person.middleName,
                lastName: payload.person.lastName,
            },
            level: payload.level,
            startDate: payload.startDate,
            endDate: payload.endDate != '' ? payload.endDate : null,
            isAlsoTrainer: payload.isAlsoTrainer
        };

        context.commit('isUpdatedInBackEnd', {
            isUpdatedInBackEnd: false,
            errorFromBackEnd: false,
            errorText: null
        }); 
        const response = await fetch(urls.memberUrl, {
            method: 'PUT', // data is overriden OR added by Firebase
            headers: {
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(member)
        });

        const resultMessage = await response.json();
        let errorFromBackEnd = false;
        let errorText = '';
        if (!resultMessage.ok) {
            context.commit('setGeneralErrorText', resultMessage.errorText);
            errorFromBackEnd = true;
            errorText = resultMessage.errorText;
        } 
        context.commit('isUpdatedInBackEnd', {
            isUpdatedInBackEnd: true,
            errorFromBackEnd,
            errorText
        }); 
     
    }
};