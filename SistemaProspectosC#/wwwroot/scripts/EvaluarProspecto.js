const app = Vue.createApp({
    data() {
        return {
            prospecto: {},
            selectedOption: '0',
            documentos: {},
            id: null,
            observacion: ""
        };
    },
    methods: {

        async updateData() {
            try {
                const respuestaProspectos = await fetch(`/Prospecto/${this.id}`)
                const Prospectos = await respuestaProspectos.json()
                console.log("RESPUESTA")
                console.log(Prospectos)
                this.prospecto = Prospectos
                this.documentos = Prospectos.documents.$values
                console.log(this.prospectos)

            } catch (error) {
                console.log(error)
            }
        },
        async cambiarEstatus() {

            if (this.selectedOption != '2') {
                this.observacion = " "
            }
            console.log("OBSERVACION")
            console.log(this.observacion)

            console.log("OPCION")
            console.log(this.selectedOption)
            try {
                const respuestaEstatus = await fetch(`/Prospecto/${this.id}/${this.selectedOption}`, {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                    , body: JSON.stringify(this.observacion)
                })
            } catch (er) {
                console.error(er)
            }

        },
        async downloadDocument(documentName) {
            const url = `/Prospecto/download/${this.id}/${documentName}`
            console.log("URL :" + url)
            try {
                const response = await fetch(url);

                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }

                const blob = await response.blob();
                const urlBlob = window.URL.createObjectURL(blob);


                window.open(urlBlob, '_blank')
            } catch (e) {
                console.error('Download error:', e.message);
            }
        }


    }, async created() {
        lista = window.location.pathname.split('/')
        this.id = lista[2]
        this.updateData()
     console.log(lista)

        console.log("ID")
        console.log(this.id)
    }
});

// Mount the Vue app to the specified element in the HTML
app.mount('#vueContainer');
