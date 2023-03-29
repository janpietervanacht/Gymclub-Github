export default {

    //------------------------------------------------------- 

    async getList(context, payload) { // een action wordt altijd 'dispatched' (hier is dat in TrainersList.Vue)
       
        context.commit('setIsLoaded', false); 
        // Haal de URL op uit het SWAGGER scherm 'Curl'
        // Deze controller 'trainer' krijgt een eigen Curl nummer = 7215
        // Case is onbelangrijk voor de route
        const response = await fetch(`https://localhost:7215/Trainer/GETList`); 
        const responseData = await response.json();
        context.commit('setIsLoaded', true); 

        if (!response.ok) {
           const error = new Error(responseData.message || 'Failed to fetch!');
           throw error;
        }

        const trainers = [];
        for (const key in responseData) {

            const trainer = {
                id: responseData[key].id,
                personId: responseData[key].personId,
                person: {
                    personId: responseData[key].person.id,
                    firstName: responseData[key].person.firstName,
                    middleName: responseData[key].person.middleName,
                    lastName: responseData[key].person.lastName,
                },
                // level: responseData[key].level,
                isCertified: responseData[key].isCertified
            };
            trainers.push(trainer);
        };

        context.commit('setTrainers', trainers); // zie local 'mutations': zet trainers in local state
    },

    //------------------------------------------------------- 

    async get(context, payload) { // een action wordt altijd 'dispatched' (hier is dat in TrainersList.Vue)

        context.commit('setIsLoaded', false); 
        // Case is onbelangrijk voor de route
        // Haal de URL op uit het SWAGGER scherm 'Curl'
        // Deze controller 'trainer' krijgt een eigen Curl nummer = 7215
        const response = await fetch('https://localhost:7215/TRainer/' + payload.id);  
        const responseData = await response.json();
        context.commit('setIsLoaded', true); 

        const trainer = {
            id: responseData.id,
            personId: responseData.personId,
            person: {
                id: responseData.person.id,
                firstName: responseData.person.firstName,
                middleName: responseData.person.middleName,
                lastName: responseData.person.lastName,
            },
            isCertified: responseData.isCertified
        };
        context.commit('setTrainer', trainer); // zie local 'mutations': zet trainers in local state
    },

    //------------------------------------------------------- 

    async delete(context, payload) {
        context.commit('isUpdatedInBackEnd', false); 
        const response = await fetch('https://localhost:7215/Trainer/' + payload, {
            method: 'DELETE', // data is overriden OR added by Firebase
            headers: {
                'Content-Type': 'application/json'
              },
            // body: JSON.stringify(trainer)
        });
        context.commit('isUpdatedInBackEnd', true); 
    },

    //------------------------------------------------------- 

    async add(context, payload) {
        const trainer = {
            id: 0,
            personId: 0,
            person: {
                id: 0,
                firstName: payload.person.firstName,
                middleName: payload.person.middleName,
                lastName: payload.person.lastName,
            },
            isCertified: payload.isCertified,
            certificates: []
        };

        context.commit('isUpdatedInBackEnd', false); 
        const response = await fetch(`https://localhost:7215/Trainer`, {
            method: 'POST', // data is overriden OR added by Firebase
            headers: {
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(trainer)
        });
        context.commit('isUpdatedInBackEnd', true); 
    },

    //------------------------------------------------------- 


    async update(context, payload) {
        const trainer = {
            id: payload.id,
            personId: payload.personId,
            person: {
                id: payload.person.id,
                firstName: payload.person.firstName,
                middleName: payload.person.middleName,
                lastName: payload.person.lastName,
            },
            isCertified: payload.isCertified,
            certificates: []
        };

        context.commit('isUpdatedInBackEnd', false); 
        const response = await fetch(`https://localhost:7215/Trainer`, {
            method: 'PUT', // data is overriden OR added by Firebase
            headers: {
                'Content-Type': 'application/json'
              },
            body: JSON.stringify(trainer)
        });
        context.commit('isUpdatedInBackEnd', true); 
    }

};