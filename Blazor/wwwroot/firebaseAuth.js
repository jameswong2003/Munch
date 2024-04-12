// Initialize Firebase
const firebaseConfig = {
    apiKey: "AIzaSyAHo2rjemjrU-08H1OSmNjhzTw63Dq_czM",
    authDomain: "munch-77a3b.firebaseapp.com",
    projectId: "munch-77a3b",
    storageBucket: "munch-77a3b.appspot.com",
    messagingSenderId: "804520378656",
    appId: "1:804520378656:web:64e96b65844902b2359745",
    measurementId: "G-7F44S2WK8Q"
  };
  const app = firebase.initializeApp(firebaseConfig);
  const auth = firebase.auth();
  
  function signUp(email, password) {
    console.log("firebaseAuth.js loaded", window.firebaseAuth);
    return auth.createUserWithEmailAndPassword(email, password);
  }
  
  function logIn(email, password) {
    return auth.signInWithEmailAndPassword(email, password);
  }
  
  function logOut() {
    return auth.signOut();
  }
  
  window.firebaseAuth = {
    signUp: function (email, password) {
      return signUp(email, password);
    },
    logIn: function (email, password) {
      return logIn(email, password);
    },
    logOut: function () {
      return logOut();
    }
  };
  