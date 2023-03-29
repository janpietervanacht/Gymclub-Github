/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

/* Een module is een sub store */
/* Member related state, mutations, actions, getters */

import mutations from './mutations.js';
import actions from './actions.js';
import getters from './getters.js';

export default {
    namespaced: true, // zie hoofd index.js voor de naam van deze namespace
    state() {
        return {
            members: [],
            member: {},
            isLoaded: false,
            isUpdatedInBackEnd: false,
            errorFromBackEnd: false,
            generalErrorText: '',
        };
    },
    mutations,
    actions,
    getters
}