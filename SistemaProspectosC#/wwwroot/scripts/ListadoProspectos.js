  const app = Vue.createApp({
    data() {
      return {
          prospectos: {},
      };
    },
    methods:{

      async updateData(){
        try {
          const respuestaProspectos = await fetch("/Prospecto")
          const Prospectos = await respuestaProspectos.json()

            this.prospectos = Prospectos.$values
            console.log(this.prospectos)

        } catch (error) {
          console.log(error)
        }
        },
        async downloadDocument(prospectoID, documentName) {
            const url = `/Prospecto/download/${prospectoID}/${documentName}`
            console.log("URL :"+url)
            try {
                const response = await fetch(url);

                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }

                const blob = await response.blob();
                const urlBlob = window.URL.createObjectURL(blob);

  
                window.open(urlBlob,'_blank')
            } catch (e) {
                console.error('Download error:', e.message);
            }
        },


      
    },    async created() {
      this.updateData()
    
    }
  });
  

  app.mount('#vueContainer');
  