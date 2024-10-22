import React, { useEffect, useState } from 'react';
import { fetchItems } from './services/apiService';

function App() {
  const [items, setItems] = useState([]);

  useEffect(() => {
    async function getItems() {
      try {
        console.log("before data");
        const data = await fetchItems();
        console.log("data: ", data);
        setItems(data);
        console.log("items: ", items);

      } catch (error) {
        console.error("Error fetching items:", error);
      }
    }

    getItems();
  }, []);

  return (
    <div className="App">
      <h1>Items List</h1>
      <ul>
        {items.map((item, index) => (
          <li key={index}>{item}</li>
        ))}
      </ul>
    </div>
  );
}

export default App;
