const API_URL = 'https://localhost:7274/api/financial-transactions';

const form = document.getElementById('uploadForm');
const fileInput = document.getElementById('fileInput');
const result = document.getElementById('result');

form.addEventListener('submit', async (event) => {
  event.preventDefault();

  if (fileInput.files.length === 0) {
    alert('Select the CNAB file before sending.');
    return;
  }

  const file = fileInput.files[0];
  await uploadFile(file);
  await loadTransactions();
});

document.addEventListener('DOMContentLoaded', async () => {
  await loadTransactions();
});

async function loadTransactions() {
  const tbody = document.getElementById('transactionsBody');
  tbody.innerHTML = '<tr><td colspan="4">Loading...</td></tr>';

  const filterBody = {
    filters: [], // without filter = returns everything
    pageNumber: 1,
    itemsPerPage: 10000
  };

  try {
    const data = await getFinancialTransactionsByFilter(filterBody);

    if (!data || data.length === 0) {
      tbody.innerHTML = '<tr><td colspan="4">No records found.</td></tr>';
      return;
    }

    tbody.innerHTML = data.resultsInPage.map(item => `
      <tr>
        <td>${item.type ?? ''}</td>
        <td>${item.occurrenceDate ?? ''}</td>
        <td>${formatCurrency(item.value) ?? ''}</td>
        <td>${item.cpf ?? ''}</td>
        <td>${item.card ?? ''}</td>
        <td>${item.storeName ?? ''}</td>
        <td>${item.storeOwner ?? ''}</td>
      </tr>
    `).join('');

  } catch (error) {
    console.error('Erro ao carregar transações:', error);
    tbody.innerHTML = '<tr><td colspan="4">Error loading transactions.</td></tr>';
  }
}

function formatCurrency(value) {
  if (value == null || isNaN(value)) return '';
  return new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
  }).format(value);
}

async function uploadFile(file) {
  const formData = new FormData();
  formData.append('file', file);

  try {
    const response = await fetch(`${API_URL}`, {
      method: 'POST',
      body: formData
    });

    if (!response.ok) throw new Error(`Erro: ${response.status}`);

    const data = await response.json();
    console.log('Upload response:', data);
    result.textContent = 'CNAB file imported successfully!';

  } catch (error) {
    console.error('Error sending file:', error);
    result.textContent = 'Failed to send the file.';
  }
}

async function getFinancialTransactionsByFilter(filterBody) {
  try {
    const response = await fetch(`${API_URL}/get-by-filter`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(filterBody)
    });

    if (!response.ok) throw new Error(`Erro: ${response.status}`);

    const data = await response.json();
    console.log('Filter response:', data);
    return data;

  } catch (error) {
    console.error('Error fetching transactions:', error);
    throw error;
  }
}

async function filterTransactions() {
  const filterBody = {
    filters: [
    //   {
    //     filterType: "Containing",
    //     filterProperty: "description",
    //     filterValue: "Supermercado"
    //   }
    ],
    pageNumber: 1,
    itemsPerPage: 10000
  };

  const resultData = await getFinancialTransactionsByFilter(filterBody); 
}
