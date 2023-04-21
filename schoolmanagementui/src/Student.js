import React from "react";
import { BaseUrl, MinimumNumberOfStudents } from "./constants";

const Student = () => {
  const [students, setStudents] = React.useState([]);

  const [teachers, setTeachers] = React.useState([]);

  const [newStudent, setNewStudent] = React.useState({
    name: "",
    dateOfBirth: "2023-01-01",
    teacherId: 0
  });

  const onChangeNewStudent = (event, prop) => {
    setNewStudent({
      ...newStudent,
      [prop]: event.target.value
    });
  };

  const onAddNewStudent = () => {
    setStudents([...students, newStudent]);
  };

  const onSaveNewStudents = () => {
    if (students.length < MinimumNumberOfStudents) {
      return alert(
        "Number of Students must be greater than " + MinimumNumberOfStudents
      );
    }
    const newStudents = students.filter((s) => !s.isSaved);

    addNewStudents(newStudents);
  };

  const addNewStudents = async (data) => {
    try {
      const response = await fetch(`${BaseUrl}/api/Student/AddStudents`, {
        method: "POST", // or 'PUT'
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      });

      const result = await response.json();

      if (result > 0) {
        alert("Save successfully.");
        getStudents();
      } else {
        alert("Save failed.");
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const getStudents = async (studentName = "") => {
    const response = await fetch(
      `${BaseUrl}/api/Student/GetStudents?studentName=${studentName}`
    );
    const jsonData = await response.json();
    const savedData = jsonData.map((s) => ({ ...s, isSaved: true }));

    setStudents(savedData);
  };

  const getTeachers = async () => {
    const response = await fetch(
      `${BaseUrl}/api/Teacher/GetTeachers?teacherName=`
    );
    const jsonData = await response.json();

    setTeachers(jsonData);
  };

  React.useEffect(() => {
    getStudents();
    getTeachers();
  }, []);

  return (
    <>
      <div className="row mt-1 mb-1">
        <div className="col-12">
          <div className="row">
            <div className="col-8">
              <h3>Add new Student</h3>
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
                      value={newStudent.name}
                      onChange={(event) => onChangeNewStudent(event, "name")}
                      className="form-control "
                      aria-label="Name"
                      aria-describedby="inputGroup-sizing-default"
                    />
                  </div>
                </div>
                <div className="col-3">
                  <div className="input-group mb-3">
                    <div className="input-group-prepend">
                      <span
                        className="input-group-text"
                        id="inputGroup-sizing-default"
                      >
                        DoB
                      </span>
                    </div>
                    <input
                      type="date"
                      value={newStudent.dateOfBirth}
                      onChange={(event) =>
                        onChangeNewStudent(event, "dateOfBirth")
                      }
                      className="form-control "
                      aria-label="DoB"
                      aria-describedby="inputGroup-sizing-default"
                    />
                  </div>
                </div>
                <div className="col-3">
                  <div className="input-group mb-3">
                    <div className="input-group-prepend">
                      <label
                        className="input-group-text"
                        htmlFor="inputGroupSelect01"
                      >
                        Teacher
                      </label>
                    </div>
                    <select
                      value={newStudent.teacherId}
                      onChange={(event) =>
                        onChangeNewStudent(event, "teacherId")
                      }
                      className="custom-select"
                      id="inputGroupSelect01"
                    >
                      <option value="0">Select teacher...</option>
                      {teachers &&
                        teachers.length > 0 &&
                        teachers.map((teacher, index) => (
                          <React.Fragment key={index}>
                            <option value={teacher.id}>{teacher.name}</option>
                          </React.Fragment>
                        ))}
                    </select>
                  </div>
                </div>
                <div className="col-3">
                  <button
                    onClick={onAddNewStudent}
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
          <h3>List of Students</h3>
        </div>
        <div className="col-3">
          <button
            onClick={onSaveNewStudents}
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
            <th>Student ID</th>
            <th>Student Name</th>
            <th>Student DoB</th>
          </tr>
        </thead>
        <tbody>
          {students &&
            students.length > 0 &&
            students.map((student, index) => (
              <tr key={index}>
                <td>{student.id}</td>
                <td>{student.name}</td>
                <td>{student.dateOfBirth.split("T")[0]}</td>
              </tr>
            ))}
        </tbody>
      </table>
    </>
  );
};

export default Student;
