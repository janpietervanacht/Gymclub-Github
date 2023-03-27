<template>
    <h4 class="knipperlicht">{{description}}</h4>
</template>

<script>
export default {
    props: {
        description: {
            type: String,
            required: false,
            default: 'ITEM'
        },
    }
}
</script>

<style scoped>

.knipperlicht {
        margin-left: 20px;
        color: red;
        font-style: italic;
        animation: blink-animation 1s steps(10, start) infinite;
        -webkit-animation: blink-animation 1s steps(10, start) infinite;
}
@keyframes blink-animation {
        to {
          visibility: hidden;
        }
}
@-webkit-keyframes blink-animation {
        to {
          visibility: hidden;
        }
}
</style>