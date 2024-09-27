import React,{ useRef } from 'react';
import { Link } from 'react-router-dom';
import './Header.css'; // Importing CSS for styling

const Header = () => {
  return (
    <div className="header-container">
      <div className="nav-buttons">
        <Link to="/" className="nav-button">Home</Link>
        <Link to="/create-student" className="nav-button">Create Students</Link>
        <Link to="/list-students" className="nav-button">List Students</Link>
      </div>
    </div>
  );
}

export default Header;
