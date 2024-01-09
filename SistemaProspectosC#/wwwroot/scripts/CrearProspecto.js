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
            this.archivos.push({}); // Adds a new empty object (representing a file input)
        },
        handleFileChange(event, index) {
            // Update the specific index in the documents array with the chosen file
            this.archivos[index] = event.target.files[0];
        },
        salir() {
            window.close()
        },
        async submitForm() {
            // Handle form submission
            const formData = new FormData();
            formData.append('prospecto', JSON.stringify(this.prospecto));
            // Append files
            this.archivos.forEach((file, index) => {
                if (file) {
                    formData.append('archivos', file);
                }
            });

            for (let [key, value] of formData.entries()) {
                console.log(key, value);
            }
            // Example: POST formData to your API endpoint
            const response = await fetch('/Prospecto', { method: 'POST', body: formData });
            console.log(response)
        },
        handleFiles(event) {
            this.archivos = Array.from(event.target.files);
        }
    }
});
app.mount('#vueContainer');