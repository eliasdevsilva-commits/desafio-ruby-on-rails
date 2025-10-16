const apiUrl = window.location.hostname === 'localhost' 
    ? 'http://localhost:8080/api/financial-transactions'
    : 'http://api:8080/api/financial-transactions';

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
  const container = document.getElementById('transactionsContainer');
  container.innerHTML = '<div>Loading...</div>';

  const filterBody = {
    filters: [], // without filter = returns everything
    pageNumber: 1,
    itemsPerPage: 10000
  };

  try {
    const data = await getFinancialTransactionsByFilter(filterBody);

    const grouped = groupByStore(data.resultsInPage);

    console.log('Grouped transactions:', grouped);

    renderStores(grouped);

  } catch (error) {
    console.error('Erro ao carregar transações:', error);
    container.innerHTML = '<div>Error loading transactions.</div>';
  }
}

function renderStores(grouped) {
  const container = document.getElementById('transactionsContainer');
  container.innerHTML = '';

  Object.entries(grouped).forEach(([store, transactions]) => {
    const totalValue = transactions.reduce((sum, t) => sum + (t.value || 0), 0);
    const totalCount = transactions.length;

    const storeDiv = document.createElement('div');
    storeDiv.className = 'store';

    const header = document.createElement('div');
    header.className = 'store-header';
    header.innerHTML = `
      <span>${store}</span>
      <span>${totalCount} transactions | ${formatCurrency(totalValue)}</span>
    `;

    const txDiv = document.createElement('div');
    txDiv.className = 'transactions';

    const table = document.createElement('table');
    table.innerHTML = `
      <thead>
        <tr>
          <th>Transaction Type</th>
          <th>Date</th>
          <th>Value</th>
          <th>CPF</th>
          <th>Card</th>
          <th>Store Owner</th>
        </tr>
      </thead>
      <tbody>
        ${transactions.map(tx => `
          <tr>
            <td>${tx.type ?? ''}</td>
            <td>${tx.occurrenceDate ?? ''}</td>
            <td>${formatCurrency(tx.value)}</td>
            <td>${tx.cpf ?? ''}</td>
            <td>${tx.card ?? ''}</td>
            <td>${tx.storeOwner ?? ''}</td>
          </tr>
        `).join('')}
      </tbody>
    `;

    txDiv.appendChild(table);

    header.addEventListener('click', () => {
      txDiv.style.display = txDiv.style.display === 'none' ? 'block' : 'none';
    });

    storeDiv.appendChild(header);
    storeDiv.appendChild(txDiv);
    container.appendChild(storeDiv);
  });
}

function groupByStore(transactions) {
  const groups = {};

  transactions.forEach(tx => {
    const store = tx.storeName || 'Unknown';
    if (!groups[store]) groups[store] = [];
    groups[store].push(tx);
  });

  return groups;
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
    const response = await fetch(`${apiUrl}`, {
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
    const response = await fetch(`${apiUrl}/get-by-filter`, {
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