const app = Vue.createApp({
    data() {
        return {
            prospecto: {
                nombre: '',
                primerApellido: '',
                segundoApellido: '',
                calle: '',
                numero: '',
                colonia: '',
                codigoPostal: '',
                telefono:'',
                rfc: ''
            },
            archivos: [{}],

        };
    },
    methods: {
        addArchivo() {
            this.archivos.push({}); 
        },
        handleFileChange(event, index) {
            this.archivos[index] = event.target.files[0];
        },
        salir() {
            window.close()
        },
        async submitForm() {
            const formData = new FormData();
            formData.append('prospecto', JSON.stringify(this.prospecto));
            this.archivos.forEach((file, index) => {
                if (file) {
                    formData.append('archivos', file);
                }
            });

            for (let [key, value] of formData.entries()) {
                console.log(key, value);
            }
            const response = await fetch('/Prospecto', { method: 'POST', body: formData });
            console.log(response)
        },
        handleFiles(event) {
            this.archivos = Array.from(event.target.files);
        }
    }
});
app.mount('#vueContainer');