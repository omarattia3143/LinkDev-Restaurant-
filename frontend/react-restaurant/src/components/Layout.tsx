import React, {Dispatch, useEffect, useState} from "react";
import Menu from "./Menu";
import axios from "axios";
import { Navigate } from "react-router-dom";
import Nav from "./Nav";
import { User } from "../models/user";
import {connect} from "react-redux";
import {setUser} from "../redux/actions/setUserAction";

const Layout = (props: any) => {
  const [redirect, setRedirect] = useState(false);
  useEffect(() => {
    (async () => {
      try {
        // const { data } = await axios.get("/user");
        const data = new User();

        data.id = 1;
        data.first_name = "";
        data.last_name = "";
        data.email = "";


        props.setUser(data)
      } catch {
        setRedirect(true);
      }
    })();
  }, []);

  if (redirect) {
    return <Navigate to={"/login"} />;
  }

  return (
    <div>
      <Nav />

      <div className="container-fluid">
        <div className="row">
          <Menu />

          <main className="col-md-9 ms-sm-auto col-lg-10 px-md-4">
            <div className="table-responsive">{props.children}</div>
          </main>
        </div>
      </div>
    </div>
  );
};

const mapStateToProps = (state:{user:User}) => ({
  user: state.user
});

const mapDispatchToProps = (dispatch: Dispatch<any>) => ({
  setUser: (user: User) => dispatch(setUser(user))
});

export default connect(mapStateToProps,mapDispatchToProps)(Layout);
