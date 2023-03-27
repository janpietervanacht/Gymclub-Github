/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

/* Member related mutations */
export default {
    setMembers(state, payload) { 
        state.members = payload;
    },
    setMember(state, payload) { 
        state.member = payload;
    },
    isLoaded(state, payload) { 
        state.isLoaded = payload;
    },
    isUpdatedInBackEnd(state, payload) { 
        state.isUpdatedInBackEnd = payload;
    },
};