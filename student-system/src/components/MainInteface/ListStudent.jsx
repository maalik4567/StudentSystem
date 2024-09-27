import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'; // Updated from useHistory to useNavigate
import './ListStudent.css';
import {Link} from 'react-router-dom'

const ListStudent = () => {
  const [students, setStudents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate(); // Updated from useHistory to useNavigate

  // Fetch student data when the component mounts
  useEffect(() => {
    const fetchStudents = async () => {
      try {
        const response = await axios.get('https://localhost:44308/api/getAllStudents'); // API to get all students
        setStudents(response.data);
      } catch (err) {
        console.error('Error fetching student data:', err);
        setError('Failed to fetch students.');
      } finally {
        setLoading(false);
      }
    };

    fetchStudents();
  }, []);

  // Function to handle deleting a student
  const handleDelete = async (studentId) => {
    if (window.confirm('Are you sure you want to delete this student?')) {
      try {
        await axios.delete(`https://localhost:44308/api/delete/${studentId}`); // API to delete a student
        setStudents(students.filter((student) => student.Id !== studentId)); // Update the state to remove deleted student
      } catch (err) {
        console.error('Error deleting student:', err);
        setError('Failed to delete the student.');
      }
    }
  };

  // Function to handle editing a student
  const handleEdit = async (studentId) => {
    try {
      const response = await axios.get(`https://localhost:44308/api/getStudent/${studentId}`); // API to get student by ID
      const studentData = response.data;

      // Navigate to the edit page with the student data
      navigate(`/edit/${studentId}`, { state: { student: studentData } }); // Updated with useNavigate
    } catch (err) {
      console.error('Error fetching student details:', err);
      setError('Failed to fetch student details.');
    }
  };

  if (loading) {
    return <div>Loading...</div>; // Optional: loading state
  }

  if (error) {
    return <div className="text-danger">{error}</div>; // Optional: error state
  }

  return (
    <div className="container table-container">
      <table className="table table-striped mt-4">
        <thead>
          <tr>
            <th>ID #</th>
            <th>Name</th>
            <th>Course</th>
            <th>Class</th>
            <th>Contact No.</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {students.length > 0 ? (
            students.map((student) => (
              <tr key={student.Id}>
                <td>{student.Id}</td>
                <td>{student.Name}</td>
                <td>{student.Courses}</td>
                <td>{student.Class}</td>
                <td>{student.ContactNumber}</td>
                <td>
                  <Link to={`/EditStudent/${student.Id}`}>
                  <button className="btn btn-sm btn-success" >
                    ✔️ Edit
                  </button>
                  </Link>
                  <button
                    className="btn btn-sm btn-danger mr-2"
                    onClick={() => handleDelete(student.Id)}
                  >
                    ❌ Delete
                  </button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="6" className="text-center">
                No students available
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ListStudent;
