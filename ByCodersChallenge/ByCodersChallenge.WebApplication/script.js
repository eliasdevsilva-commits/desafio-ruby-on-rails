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

header.addEventListener('click', () => {
  const isVisible = txDiv.style.display !== 'none';
  txDiv.style.display = isVisible ? 'none' : 'block';
  header.classList.toggle('active', !isVisible);
});

document.addEventListener('DOMContentLoaded', async () => {
  await loadTransactions();
});

async function loadTransactions() {
    const container = document.getElementById('transactionsContainer');
    const totalDiv = document.getElementById('totalBalance');
    container.innerHTML = '<div>Loading...</div>';
    totalDiv.textContent = '';

    const filterBody = {
        filters: [],
        pageNumber: 1,
        itemsPerPage: 10000,
    };

    try {
        const data = await getFinancialTransactionsByFilter(filterBody);
        const transactions = data.resultsInPage;
      
        const totalGeneral = transactions.reduce((sum, t) => sum + (t.value || 0), 0);
        totalDiv.innerHTML = `Total Balance: <span class="${totalGeneral < 0 ? 'negative' : 'positive'}">${formatCurrency(totalGeneral)}</span>`;

        const grouped = groupByStore(transactions);
        renderStores(grouped);
    } catch (error) {
        console.error('Erro ao carregar transações:', error);
        container.innerHTML = '<div>Error loading transactions.</div>';
        totalDiv.textContent = '';
    }
}


function renderStores(grouped) {
  const container = document.getElementById('transactionsContainer');
  container.innerHTML = '';

  Object.entries(grouped).forEach(([store, transactions]) => {
    const totalValue = transactions.reduce((sum, t) => sum + (t.value || 0), 0);
    const totalCount = transactions.length;
    const ownerName = transactions[0]?.storeOwner ?? 'Unknown';

    const storeDiv = document.createElement('div');
    storeDiv.className = 'store';

    const header = document.createElement('div');
    header.className = 'store-header';
    header.innerHTML = `
      <div class="store-info">
        <span class="store-name">${store}</span>
        <span class="store-owner">Owner: ${ownerName}</span>
      </div>
      <div class="store-summary">
        <span>${totalCount} transactions | Total balance: <strong class="${totalValue < 0 ? 'negative' : 'positive'}">${formatCurrency(totalValue)}</strong></span>
        <span class="arrow">▼</span>
      </div>
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
        </tr>
      </thead>
      <tbody>
        ${transactions.map(tx => `
          <tr>
            <td>${tx.type ?? ''}</td>
            <td>${tx.occurrenceDate ?? ''}</td>
            <td class="${tx.value < 0 ? 'negative' : 'positive'}">${formatCurrency(tx.value)}</td>
            <td>${tx.cpf ?? ''}</td>
            <td>${tx.card ?? ''}</td>
          </tr>
        `).join('')}
      </tbody>
    `;

    txDiv.appendChild(table);
    txDiv.style.display = 'none';

    header.addEventListener('click', () => {
      const visible = txDiv.style.display === 'block';
      txDiv.style.display = visible ? 'none' : 'block';
      header.querySelector('.arrow').textContent = visible ? '▼' : '▲';
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