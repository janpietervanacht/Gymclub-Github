// globalMixins gebruik je maar weinig, en vaak als een ontwikkelaar. 
// Je kan bijvoorbeeld een logging bijhouden, of analytische zaken

export default {
    mounted() {
        console.log('Global Mixin says: Mounted...'); // check de console
    }
}