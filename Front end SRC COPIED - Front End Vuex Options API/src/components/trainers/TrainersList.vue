<template>
    <h1>Trainers List work</h1>
</template>

<script>

export default {
    created() { // lifecycle hook
        this.$store.dispatch('setCurrentList', 'trainers');
    }
}

</script>