
document.addEventListener('DOMContentLoaded', function (event) {
    let app = new Vue({ /*view*/
        el: '#app',         //document.getElementById('view'),
     //  mounted: function () { },
        data: {
            NewUserName: "",
            NewEmail: "",
            Password: "",
            NewPassword: "",
            ConfirmNewPassword: ""
        },
        methods:
        {
            checkForm: function (e) { //save
              //  try {
             //   var url = '/User/ChangeAllData';
                     var data = {
                        "NewUserName": this.NewUserName,
                        "NewEmail": this.NewEmail,
                        "Password": this.Password,
                        "NewPassword": this.NewPassword,
                        "ConfirmNewPassword": this.ConfirmNewPassword
                    };
                    e.preventDefault();
                    alert("successful");
                  /*  var self = this;
                    http.post(url, data)
                        .then(function (response) {
                            console.log("successfully added!");
                        })
                        .catch(function (error) {
                            console.log(error);
                        });*/
               // } catch (ex) {
               //     console.log(ex);
               // }

                return false;
            }
        }
    });
});


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











var app6 = new Vue({
    el: '#app-6',
    data: {
        message: 'Привет, Vue!'
    }
});

//<script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"> </script>
   
