import React from "react";
import { BaseUrl, MinimumNumberOfTeachers } from "./constants";

const Teacher = () => {
  const [teachers, setTeachers] = React.useState([]);

  const [newTeacher, setNewTeacher] = React.useState({
    name: ""
  });

  const onChangeNewTeacher = (event, prop) => {
    setNewTeacher({
      ...newTeacher,
      [prop]: event.target.value
    });
  };

  const onAddNewTeacher = () => {
    setTeachers([...teachers, newTeacher]);
  };

  const onSaveNewTeachers = () => {
    if (teachers.length < MinimumNumberOfTeachers) {
      return alert(
        "Number of Teachers must be greater than " + MinimumNumberOfTeachers
      );
    }
    const newTeachers = teachers.filter((t) => !t.isSaved);

    addNewTeachers(newTeachers);
  };

  const addNewTeachers = async (data) => {
    try {
      const response = await fetch(`${BaseUrl}/api/Teacher/AddTeachers`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      });

      const result = await response.json();

      if (result > 0) {
        alert("Save successfully.");
        getTeachers();
      } else {
        alert("Save failed.");
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const getTeachers = async (teacherName = "") => {
    const response = await fetch(
      `${BaseUrl}/api/Teacher/GetTeachers?teacherName=${teacherName}`
    );
    const jsonData = await response.json();
    const savedData = jsonData.map((s) => ({ ...s, isSaved: true }));

    setTeachers(savedData);
  };

  React.useEffect(() => {
    getTeachers();
  }, []);

  return (
    <>
      <div className="row mt-1 mb-1">
        <div className="col-12">
          <div className="row">
            <div className="col-8">
              <h3>Add new Teacher</h3>
            </div>
            <div className="col-4"></div>
          </div>
          <div className="row">
            <div className="col-12">
              <div className="row">
                <div className="col-3">
                  <div className="input-group mb-3">
                    <div className="input-group-prepend">
                      <span
                        className="input-group-text"
                        id="inputGroup-sizing-default"
                      >
                        Name
                      </span>
                    </div>
                    <input
                      type="text"
                      value={newTeacher.name}
                      onChange={(event) => onChangeNewTeacher(event, "name")}
                      className="form-control "
                      aria-label="Name"
                      aria-describedby="inputGroup-sizing-default"
                    />
                  </div>
                </div>
                <div className="col-3"></div>
                <div className="col-3"></div>
                <div className="col-3">
                  <button
                    onClick={onAddNewTeacher}
                    type="button"
                    className="btn btn-primary w-100"
                  >
                    Add
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="row mt-1 mb-1">
        <div className="col-9">
          <h3>List of Teachers</h3>
        </div>
        <div className="col-3">
          <button
            onClick={onSaveNewTeachers}
            type="button"
            className="btn btn-primary w-100"
          >
            Save
          </button>
        </div>
      </div>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Teacher ID</th>
            <th>Teacher Name</th>
          </tr>
        </thead>
        <tbody>
          {teachers &&
            teachers.length > 0 &&
            teachers.map((teacher, index) => (
              <tr key={index}>
                <td>{teacher.id}</td>
                <td>{teacher.name}</td>
              </tr>
            ))}
        </tbody>
      </table>
    </>
  );
};

export default Teacher;
