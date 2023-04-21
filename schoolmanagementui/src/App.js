import "./styles.css";

import Dashboard from "./Dashboard";
import Student from "./Student";
import Teacher from "./Teacher";
import React from "react";

export default function App() {
  const [showDashboard, setShowDashboard] = React.useState(true);
  const [showStudent, setShowStudent] = React.useState(false);
  const [showTeacher, setShowTeacher] = React.useState(false);

  const showScreen = (dashboard, student, teacher) => {
    setShowDashboard(dashboard);
    setShowStudent(student);
    setShowTeacher(teacher);
  };

  return (
    <div className="container">
      <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <a
          onClick={() => showScreen(true, false, false)}
          className="navbar-brand"
        >
          School Management
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item active">
              <a
                onClick={() => showScreen(true, false, false)}
                className="nav-link"
              >
                <span>Dashboard</span>
              </a>
            </li>
            <li className="nav-item active">
              <a
                onClick={() => showScreen(false, true, false)}
                className="nav-link"
              >
                <span>Student</span>
              </a>
            </li>
            <li className="nav-item active">
              <a
                onClick={() => showScreen(false, false, true)}
                className="nav-link"
              >
                <span>Teacher</span>
              </a>
            </li>
          </ul>
        </div>
      </nav>
      {showDashboard && <Dashboard />}
      {showStudent && <Student />}
      {showTeacher && <Teacher />}
    </div>
  );
}
