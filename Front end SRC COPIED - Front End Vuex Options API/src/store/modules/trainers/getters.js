/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

/* Trainer related getters */

// In 'mutations' verander je state-variabelen
// In 'getters' haal je state-variabelen op
export default {
    trainers(state) {
        return state.trainers;
    },
    trainer(state) {
        return state.trainer;
    },
    isLoaded(state) {
        return state.isLoaded;
    },
    isUpdatedInBackEnd(state) {
        return state.isUpdatedInBackEnd;
    }
};