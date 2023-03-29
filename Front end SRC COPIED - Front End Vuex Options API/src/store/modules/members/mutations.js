/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

/* Member related mutations */
export default {
    setMembers(state, payload) { 
        state.members = payload;
    },
    setMember(state, payload) { 
        state.member = payload;
    },
    setIsLoaded(state, payload) { 
        state.isLoaded = payload;
    },
    // er is geen aparte mutation nodig voor errorFromBackEnd
    isUpdatedInBackEnd(state, payload) { 
        state.isUpdatedInBackEnd = payload.isUpdatedInBackEnd;
        state.errorFromBackEnd = payload.errorFromBackEnd;
        state.generalErrorText = payload.errorText;
    },
    setGeneralErrorText(state, payload) {
        state.generalErrorText = payload;
    },
};