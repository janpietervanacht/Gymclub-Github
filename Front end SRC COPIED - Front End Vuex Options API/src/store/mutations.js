export default {
    setCurrentList(state, payload) { 
        state.currentList = payload;
    },
    
    // recoverBackEndIsBusy gaat over de gehele back-end, daarom in deze main store 
    recoverBackEndIsBusy(state, payload) { 
        state.recoverBackEndIsBusy = payload;
    },
}