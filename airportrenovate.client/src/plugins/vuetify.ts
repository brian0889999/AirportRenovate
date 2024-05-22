import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import { zhHant } from 'vuetify/locale';

const theme = {
    dark: false,
    colors: {
        primary: '#3f51b5', // �Ŧ�
        'primary-darken-1': '#0D47A1', // �`�Ŧ�
        secondary: '#03A9F4', // �G�Ŧ�
        'secondary-darken-1': '#01579B', // �`�G�Ŧ�
        accent: '#9C27b0', // ����
        info: '#00CAE3', // �C��
        success: '#4CAF50', // ���
        warning: '#FB8C00', // ���
        error: '#F44336', // ����
        background: '#121212', // �`�Ǧ�I��
        surface: '#1e1e1e', // �`�ŦǦ��
    },
};

const vuetify = createVuetify({
    components,
    directives,
    theme: {
        defaultTheme: 'light',
        themes: {
            theme,
        },
    },
    locale: {
        locale: 'zhHant',
        messages: { zhHant },
    },
})

export default vuetify;