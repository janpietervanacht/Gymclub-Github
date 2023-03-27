export default {
    setCurrentList(context, payload) {
        context.commit('setCurrentList', payload); // zie local 'mutations': zet members in local state
    },

    async recover(context, payload) { // payload = 'member' of 'trainer' = naam controller in back-end
        context.commit('recoverBackEndIsBusy', true); 
        await fetch(`https://localhost:7179/${payload}/recover`);
        context.commit('recoverBackEndIsBusy', false); 
    },
}