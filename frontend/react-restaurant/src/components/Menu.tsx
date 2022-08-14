import React from "react";
import { NavLink } from "react-router-dom";

const Menu = () => {
  return (
    <div>
      <nav
        id="sidebarMenu"
        className="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse"
      >
        <div className="position-sticky pt-3">
          <ul className="nav flex-column">
            <li className="nav-item">
              <NavLink
                to={"/branch"}
                className="nav-link active"
                aria-current="page"
              >
                Branches
              </NavLink>
            </li>

            <li className="nav-item">
              <NavLink
                to={"/booking"}
                className="nav-link active"
                aria-current="page"
              >
                Booking
              </NavLink>
            </li>
          </ul>
        </div>
      </nav>
    </div>
  );
};

export default Menu;
