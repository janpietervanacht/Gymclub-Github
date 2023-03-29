/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

// In 'mutations' verander je state-variabelen
// In 'getters' haal je state-variabelen op

/* Member related mutations */
export default {
    setTrainers(state, payload) { 
        state.trainers = payload;
    },
    setTrainer(state, payload) { 
        state.trainer = payload;
    },
    setIsLoaded(state, payload) { 
        state.isLoaded = payload;
    },
    isUpdatedInBackEnd(state, payload) { 
        state.isUpdatedInBackEnd = payload;
    },
};