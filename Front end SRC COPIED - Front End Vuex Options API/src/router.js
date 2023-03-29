import { createRouter, createWebHistory} from 'vue-router';

import MembersList from './components/members/MembersList.vue';
import MemberDetail from './components/members/MemberDetail.vue';
import TrainerDetail from './components/trainers/TrainerDetail.vue';
import TrainersList from './components/trainers/TrainersList.vue';
import NotFound from './components/layout/NotFound.vue';

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {path: '/', redirect: '/members'}, // als men alleen het domein intikt, spring dan naar de ledenlijst van de gymclub
        {path: '/members', component: MembersList},
        {path: '/members/:id', props: true, component: MemberDetail},  // let op! props aanwezig omdat MemberDetail props heeft
        {path: '/trainers', component: TrainersList},
        {path: '/trainers/:id', props: true, component: TrainerDetail},  // let op! props aanwezig omdat MemberDetail props heeft
        {path: '/:notFound(.*)', component: NotFound}
    ]
});

export default router;