import logo from './logo.svg';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from '../src/components/Header/Header.jsx'; // Import your Header component
import CreateStudent from "../src/components/MainInteface/CreateStudent.jsx"
import ListStudents from '../src/components/MainInteface/ListStudent.jsx';
import Home from "../src/components/HomePage/Home.jsx"
import EditStudent from "../src/components/MainInteface/EditStudent.jsx"
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <div>
           <Router>
      <Header />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/create-student" element={<CreateStudent />} />
        <Route path="/list-students" element={<ListStudents />} />
        <Route path="/EditStudent/:id" element={<EditStudent />} />
      </Routes>
    </Router>
    </div>
  );
}

export default App;
