export default {
    setCurrentList(context, payload) {
        context.commit('setCurrentList', payload); // zie local 'mutations': zet members in local state
    },

    

    // De recover action werkt voor een 'members' als 'trainers'
    // Echter ik heb Herstel Alle Trainers weggehaald uit de trainers-lijst ivm complexiteit voor de Members.IsAlsoTrainer.
    async recover(context, payload) { // payload = 'member' of 'trainer' = naam controller in back-end
        context.commit('recoverBackEndIsBusy', true); 

        let localHostNr='';
        switch(payload) {
            case 'member':
                localHostNr = '7179';
                break;
            case 'trainer':
                localHostNr = '7215';
                break;
        }
        // await fetch(`https://localhost:7179/${payload}/recover`); // werkt alleen voor members
        await fetch(`https://localhost:${localHostNr}/${payload}/recover`);

        context.commit('recoverBackEndIsBusy', false); 
    },
}