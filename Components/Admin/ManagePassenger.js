import React, { useEffect, useState } from 'react';
import './AdminDashboard.css';
import Navbar from './Partials/Navbar';
import Footer from './Partials/Footer';
import { Link } from 'react-router-dom';

function ManagePassenger() {
  const [passengers, setPassengers] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('http://localhost:5084/PassengerAPI/SystemPassenger')
      .then(res => {
        if(!res.ok) {
          throw new Error('Failed to fetch passengers');
        }
        return res.json();
      })
      .then(data => {
        console.log('Passengers data:', data); // Log the data received from the API
        setPassengers(data);
      })
      .catch(error => {
        if (error instanceof SyntaxError) {
          console.error('Invalid JSON response:', error); // Log the specific error
          setError('Invalid JSON response from server');
        } else {
          console.error('Error fetching passengers:', error); // Log any other errors
          setError('Failed to fetch passengers. Please try again later.');
        }
      });
  }, []);

  return (
    <div className="main-content">
      <h1>Manage Passengers</h1>
      <br/><br/><br/>

      {/* Display error message if any */}
      {error && <p>{error}</p>}

      <br/><br/><br/>
      {/* Button to add passenger */}
      <Link to="/admin/add-passenger" className="btn-primary"> Add Passenger </Link>
      <br/>
      <br/>
      <div className="table-responsive">
        <table className="table table-full">
          <thead>
            <tr>
              <th>ID</th>
              <th>PassengerName</th>
              <th>PassenegerID</th>
              <th>Total</th>
              <th>From</th>
              <th>To</th>
              <th>TrainName</th>
              <th>Class</th>
              <th>DateTime</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {/* Loop through passenger data */}
            {passengers.map(passenger => (
              <tr key={passenger.id}>
                <td>{passenger.id}</td>
                <td>{passenger.PassengerName}</td>
                <td>{passenger.PassenegerID}</td>
                <td>{passenger.total}</td>
                <td>{passenger.From}</td>
                <td>{passenger.To}</td>
                <td>{passenger.TrainName}</td>
                <td>{passenger.Class}</td>
                <td>{passenger.DateTime}</td>
                <td>
                  <Link to={`/admin/passenger/${passenger.id}`} className="btn-secondary">Update Passenger</Link>
                  <button className="btn-danger">Delete Passenger</button>
                </td>
              </tr>
            ))}
            {/* If no passenger data found */}
            {!passengers.length && !error && <tr><td colSpan="7">No passenger data found.</td></tr>}
          </tbody>
        </table>
      </div>
    </div>
  );
}


export default ManagePassenger;
