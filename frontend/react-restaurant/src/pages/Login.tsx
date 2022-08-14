import React, { SyntheticEvent, useState } from "react";
import "../styles/Login.css";
import axios from "axios";
import {Link, Navigate} from "react-router-dom";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [navigate, setNavigate] = useState(false);

  const submit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const {data} = await axios.post("authenticate/login", {
      email,
      password,
    });

    localStorage.setItem("jwt",data.token)
    console.log(data);
    setNavigate(true);
  };

  if (navigate) {
    return <Navigate to={"/branch"} />;
  }

  return (
    <main className="form-signin w-100 m-auto">
      <form onSubmit={submit}>
        <h1 className="h3 mb-3 fw-normal">Please sign in</h1>

        <div className="form-floating">
          <input
            type="email"
            className="form-control"
            id="floatingInput"
            placeholder="name@example.com"
            onChange={(e) => setEmail(e.target.value)}
          />
          <label htmlFor="floatingInput">Email address</label>
        </div>
        <div className="form-floating">
          <input
            type="password"
            className="form-control"
            id="floatingPassword"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
          />
          <label htmlFor="floatingPassword">Password</label>
        </div>

        <button className="w-100 btn btn-lg btn-primary mt-3" type="submit">
          Sign in
        </button>

        <Link to={"/booking"} className="p-2 text-black text-decoration-underline mt-3">
          Book without an account
        </Link>
        <p className="mt-5 mb-3 text-muted">&copy; 2017–2022</p>

      </form>
    </main>
  );
};

export default Login;
