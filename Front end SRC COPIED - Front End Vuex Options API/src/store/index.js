/* Alle files die eindigen op .js moeten minimaal een export default {} kennen 

/* Main store and modules */
/* root store index.js file */

import { createStore } from 'vuex';
import membersModule from './modules/members/index.js'; // De naam coachesModule kan je vrij kiezen - is een sub store voor entiteit Coaches
import trainersModule from './modules/trainers/index.js'; // - is een sub store voor entiteit Requests
import actions from './actions.js';
import getters from './getters.js';
import mutations from './mutations.js';

const store = createStore({
    modules: {
        members: membersModule, // 'members' is de namespace van deze module (=substore) -- ook wel key genoemd
        trainers: trainersModule 
    },
    state() {
        return {
            currentList: 'member',
            recoverBackEndIsBusy: false
        };
    },
    getters,
    actions,
    mutations
});

export default store; // Niet vergeten!