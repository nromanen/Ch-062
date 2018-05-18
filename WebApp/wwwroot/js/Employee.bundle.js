
let app = new Vue({
    el: '#app',
    data: {
        errors: [],
        OldPassword: "",
        NewPassword: "",
        ConfirmNewPassword: ""

    },

    methods:
    {
        save: function (e) {
            this.errors = [];
        
           
            if (!this.OldPassword) this.errors.push("Please fill the 'Old Password' field");
            if (!this.NewPassword) this.errors.push("Please fill the 'New Password' field");
            if (!this.ConfirmNewPassword) this.errors.push("Please fill the 'Confirm New password' field");

            //if (this.OldPassword === "" || this.NewPassword === "" || this.ConfirmNewPassword === "") {
            //    this.errors.push("")
            //}

            if (!this.errors.length) return true;
            e.preventDefault();
        }

    }
});
//});

//const app = new Vue({
//    el: '#app',
//    data: {
//        name: null,
//        email: null,
//        password: null,
//        NewPassword: null
//    },
//    methods: {
//        checkForm: function (e) {
//         //   this.errors = [];
//          //  if (!this.name) this.errors.push("Укажите имя.");
//          //  if (!this.email) {
//           //     this.errors.push("Укажите электронную почту.");
//          //  } else if (!this.validEmail(this.email)) {
//          //      this.errors.push("Укажите корректный адрес электронной почты.");
//          //  }
//            if (!this.errors.length) return true;
//            e.preventDefault();
//        },
//        validEmail: function (email) {
//            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
//            return re.test(email);
//        }
//    }
//})







//<script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"> </script>
   
