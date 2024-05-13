import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { zhHant } from 'vuetify/locale';

const vuetify = createVuetify({
    components,
    directives,
    locale: {
        locale: 'zhHant',
        messages: { zhHant },
    },
})

export default vuetify;