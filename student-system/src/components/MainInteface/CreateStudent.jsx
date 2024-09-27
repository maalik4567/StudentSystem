import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './CreateStudent.css'; // Import CSS for CreateStudent component
import 'bootstrap/dist/css/bootstrap.min.css';

const CreateStudent = () => {
  const [name, setName] = useState('');
  const [selectedCourses, setSelectedCourses] = useState([]);
  const [studentClass, setStudentClass] = useState('');
  const [contactNumber, setContactNumber] = useState('');
  const [availableCourses, setAvailableCourses] = useState([]);
  const [classes, setClasses] = useState([]);
  const [errors, setErrors] = useState({});

  // Fetch courses and classes when the component mounts
  useEffect(() => {
    const fetchCoursesAndClasses = async () => {
      try {
        const coursesResponse = await axios.get('https://localhost:44308/api/courses');
        const classesResponse = await axios.get('https://localhost:44308/api/classes');

        setAvailableCourses(coursesResponse.data);
        setClasses(classesResponse.data);
      } catch (error) {
        console.error('Error fetching data:', error);
        setErrors({ fetchError: 'Failed to fetch courses and classes' });
      }
    };

    fetchCoursesAndClasses();
  }, []);

  const addCourse = (courseId, courseName) => {
    if (!selectedCourses.find(course => course.id === courseId)) {
      setSelectedCourses([...selectedCourses, { id: courseId, name: courseName }]);
    } else {
      alert('This course is already added.');
    }
  };

  const removeLastCourse = () => {
    setSelectedCourses(selectedCourses.slice(0, -1)); // Remove the last added course
  };

  const resetForm = () => {
    setName('');
    setSelectedCourses([]);
    setStudentClass('');
    setContactNumber('');
    setErrors({});
  };

  const validateForm = () => {
    let isValid = true;
    const newErrors = {};

    if (!name.trim()) {
      newErrors.nameError = 'Please enter the student\'s name.';
      isValid = false;
    }

    if (!studentClass) {
      newErrors.classError = 'Please select a class.';
      isValid = false;
    }

    if (selectedCourses.length === 0) {
      newErrors.courseError = 'Please select at least one course.';
      isValid = false;
    }

    setErrors(newErrors);
    return isValid;
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (validateForm()) {
      const studentData = {
        student: {
          Name: name,
          ContactNumber: contactNumber,
          Class: studentClass,
        },
        MarkedCourses: selectedCourses.map(course => course.id),
      };

      console.log('Submitting Data:', studentData); // For debugging

      axios.post('https://localhost:44308/api/createStudent', studentData)
        .then(response => {
          console.log(response);
          resetForm(); // Reset the form on successful submission
        })
        .catch(error => {
          console.error('Submission error:', error);
          // Optionally handle the error more specifically
        });
    }
  };

  return (
    <div className="d-flex justify-content-center formapp">
      <form onSubmit={handleSubmit} id="studentForm">
        {/* Student Name */}
        <div className="form-row mb-3">
          <div className="col-md-3 label-width">
            <label htmlFor="Name" className="col-form-label">Student Name</label>
          </div>
          <div className="col-md-9">
            <input
              type="text"
              id="Name"
              name="Name"
              className="form-control form-control-sm"
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Enter Student Name"
            />
            {errors.nameError && <div className="text-danger">{errors.nameError}</div>}
          </div>
        </div>

        {/* Course Selection */}
        <div className="form-row mb-3">
          <div className="col-md-3 label-width">
            <label htmlFor="availableCourses" className="col-form-label">Select Course</label>
          </div>
          <div className="col-md-9 course-controls">
            <select id="availableCourses" className="form-control form-control-sm">
              <option value="">Select Course</option>
              {availableCourses.map(course => (
                <option key={course.Value} value={course.Value}>
                  {course.Text}
                </option>
              ))}
            </select>
            <button
              type="button"
              className="btn btn-success mx-2"
              onClick={() => {
                const selectedCourseId = document.getElementById('availableCourses').value;
                const selectedCourseText = availableCourses.find(course => course.Value === selectedCourseId)?.Text;
                if (selectedCourseId) {
                  addCourse(selectedCourseId, selectedCourseText);
                }
              }}
            >
              +
            </button>
            <button
              type="button"
              className="btn btn-danger"
              onClick={removeLastCourse}
            >
              -
            </button>
          </div>
          {errors.courseError && <div className="text-danger">{errors.courseError}</div>}
        </div>

        {/* Selected Courses */}
        <div className="form-row mb-3">
          <div className="col-md-3 label-width">
            <label className="col-form-label">Selected Courses</label>
          </div>
          <div className="col-md-9 course-box">
            <div id="selectedCourses">
              {selectedCourses.map(course => (
                <span key={course.id} className="course-tag">
                  {course.name}
                </span>
              ))}
            </div>
          </div>
        </div>

        {/* Class Selection */}
        <div className="form-row mb-3">
          <div className="col-md-3 label-width">
            <label htmlFor="Class" className="col-form-label">Class</label>
          </div>
          <div className="col-md-9">
            <select
              id="Class"
              name="Class"
              className="form-control form-control-sm"
              value={studentClass}
              onChange={(e) => setStudentClass(e.target.value)}
            >
              <option value="">Select Class</option>
              {classes.map(cls => (
                <option key={cls.Value} value={cls.Value}>
                  {cls.Text}
                </option>
              ))}
            </select>
            {errors.classError && <div className="text-danger">{errors.classError}</div>}
          </div>
        </div>

        {/* Contact Number */}
        <div className="form-row mb-3">
          <div className="col-md-3 label-width">
            <label htmlFor="ContactNumber" className="col-form-label">Contact Number</label>
          </div>
          <div className="col-md-9">
            <input
              type="tel"
              id="ContactNumber"
              name="ContactNumber"
              className="form-control form-control-sm"
              value={contactNumber}
              onChange={(e) => setContactNumber(e.target.value)}
              placeholder="Enter Contact Number"
            />
          </div>
        </div>

        {/* Submit and Reset Buttons */}
        <div className="form-row">
          <div className="col-md-3"></div>
          <div className="col-md-9">
            <button type="submit" className="btn submit-btn">Submit</button>
            <button type="button" className="btn reset-btn mx-lg-5" onClick={resetForm}>Reset</button>
          </div>
        </div>
      </form>
    </div>
  );
};

export default CreateStudent;
