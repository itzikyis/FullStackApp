const API_URL = "http://localhost:5000/api/items"; // Ensure this matches your backend URL and port

export async function fetchItems() {
    const response = await fetch(API_URL);
    if (!response.ok) {
        throw new Error("Failed to fetch items");
    }
    return await response.json();
}
