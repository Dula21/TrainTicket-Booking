import React, { useEffect, useState } from 'react';
import './AdminDashboard.css';
import Navbar from './Partials/Navbar';
import Footer from './Partials/Footer';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';


function AdminDashboard() {
  const [trains, setTrains] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5084/TrainAPI/SystemTrain')
      .then(res => {
        if (!res.ok) {
          throw new Error('Failed to fetch trains');
        }
        return res.json();
      })
      .then(data => {
        console.log('Trains data:', data); // Log the data received from the API
        setTrains(data);
      })
      .catch(error => {
        if (error instanceof SyntaxError) {
          console.error('Invalid JSON response:', error); // Log the specific error
          setError('Invalid JSON response from server');
        } else {
          console.error('Error fetching trains:', error); // Log any other errors
          setError('Failed to fetch trains. Please try again later.');
        }
      });
  }, []);

  return (
    <div className="main-content">
      <Navbar />
      <div className="wrapper">
        <h1>Manage Trains</h1>
        <br/><br/><br/>

        {/* Display error message if any */}
        {error && <p>{error}</p>}

        <br/><br/><br/>
        {/*button to add train*/}
        <Link to="/Admin/ManagePassenger" className="btn-primary"> Manage Passengers </Link>
        <br/>
        <br/>
        <div className="table-responsive">
          <table className="table table-full">
            <thead>
              <tr>
                <th>ID</th>
                <th>Train Name</th>
                <th>Number of Seats</th>
                <th>Price</th>
                <th>From </th>
                <th>To </th>
                <th>Date</th>
                <th>CreatedAt</th>
                <th>UpdatedAt</th>
                <th>ConfidentialComment</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {/* Loop through train data */}
              {trains.map(train => (
                <tr key={train.id}>
                  <td>{train.id}</td>
                  <td>{train.name}</td>
                  <td>{train.numberOfSeats}</td>
                  <td>{train.price}</td>
                  <td>{train.fromStation}</td>
                  <td>{train.toStation}</td>
                  <td>{train.dateOfJourney}</td>
                  <td>{train.createdAt}</td>
                  <td>{train.updatedAt}</td>
                  <td>{train.confidentialComment}</td>
                  <td>
                    <button className="btn-secondary">Update Train</button>
                    <button className="btn-danger">Delete Train</button>
                  </td>
                </tr>
              ))}
              {/* If no train data found */}
              {!trains.length && !error && <tr><td colSpan="11">No train data found.</td></tr>}
            </tbody>
          </table>
        </div>
      </div>
      <Footer />
    </div>
  );
}
export default AdminDashboard;
