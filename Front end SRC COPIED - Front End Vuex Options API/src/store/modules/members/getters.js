/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

/* Member related getters */
export default {
    members(state) {
        return state.members;
    },
    member(state) {
        return state.member;
    },
    isLoaded(state) {
        return state.isLoaded;
    },
    isUpdatedInBackEnd(state) {
        return state.isUpdatedInBackEnd;
    },
    generalErrorText(state) { 
        return state.generalErrorText;
    },
    errorFromBackEnd(state) { 
        return state.errorFromBackEnd;
    },
};