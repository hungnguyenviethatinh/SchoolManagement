import React from "react";
import { BaseUrl } from "./constants";

const Dashboard = () => {
  const [data, setData] = React.useState([]);

  React.useEffect(() => {
    const getDashboard = async () => {
      const response = await fetch(`${BaseUrl}/api/Dashboard/GetDashboard`);
      const jsonData = await response.json();

      setData(jsonData);
    };

    getDashboard();
  }, []);

  return (
    <>
      <div className="row mt-1 mb-1">
        <div className="col-8">
          <h3>List of Teacher and Student</h3>
        </div>
        <div className="col-4"></div>
      </div>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Teacher ID</th>
            <th>Teacher Name</th>
            <th>Student ID</th>
            <th>Student Name</th>
            <th>Student DoB</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.length > 0 &&
            data.map((teacher, index) => (
              <React.Fragment key={index}>
                {teacher &&
                  teacher.students &&
                  teacher.students.map((student, index) => (
                    <tr key={index}>
                      <td>{teacher.id}</td>
                      <td>{teacher.name}</td>
                      <td>{student.id}</td>
                      <td>{student.name}</td>
                      <td>{student.dateOfBirth.split("T")[0]}</td>
                    </tr>
                  ))}
              </React.Fragment>
            ))}
        </tbody>
      </table>
    </>
  );
};

export default Dashboard;
